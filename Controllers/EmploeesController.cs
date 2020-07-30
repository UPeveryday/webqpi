using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Routines.Api.Entities;
using Routines.Api.Models;
using Routines.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Controllers
{

    [ApiController]
    [Route("api/companies/{companyId}/emploees")]
    public class EmploeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public EmploeesController(IMapper mapper, ICompanyRepository companyRepository)
        {
            this._mapper = mapper;
            this._companyRepository = companyRepository;
        }

        [HttpGet]
        //搜索与过滤
        //http://localhost:5000/api/companies/bbdee09c-089b-4d30-bece-44df5923716c/emploees?gender=男&q=Vince
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCanpany(Guid companyid, [FromQuery(Name = "gender")] string gendDisplay, string q)
        {
            if (!await _companyRepository.CompanyExitsAsync(companyid)) return NotFound();

            var employees = await _companyRepository.GetEmployeesAsync(companyid, gendDisplay, q);

            var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(dtos);
        }


        [HttpGet("{empolyid}", Name = nameof(GetEmployeeForCanpany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCanpany(Guid companyid, Guid empolyid)
        {
            if (!await _companyRepository.CompanyExitsAsync(companyid)) return NotFound();
            var employ = await _companyRepository.GetEmployeeAsync(companyid, empolyid);
            if (employ == null) return NotFound();
            var dtos = _mapper.Map<EmployeeDto>(employ);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<Emloyee>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employeeAddDto)
        {
            if (!await _companyRepository.CompanyExitsAsync(companyId))
            {
                return NotFound();
            }
            var entity = _mapper.Map<Emloyee>(employeeAddDto);

            _companyRepository.AddEmployee(entity, companyId);

            await _companyRepository.SaveAsync();

            var dto = _mapper.Map<EmployeeDto>(entity);

            return CreatedAtRoute(nameof(GetEmployeeForCanpany), new { companyid = companyId, empolyid = dto.Id }, dto);//调用getcompany方达，填写路由

        }

        [HttpPut("{employeeId}")]//整体替换
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeUpdateDto employeeUpdateDto)
        {

            //EmployeeUpdateDto忽略了2个Id跟新时会被忽略
            //EmployeeUpdateDto如果属性确实委会默认值
            if (!await _companyRepository.CompanyExitsAsync(companyId))
            {
                return NotFound();
            }
            var employentity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employentity == null)
            {
                //如果不存在此员工创建一个  employeeUpdateDto转为实体类模型
                var employaddentity = _mapper.Map<Emloyee>(employeeUpdateDto);
                employaddentity.Id = employeeId;
                 _companyRepository.AddEmployee( employaddentity, companyId);
                await _companyRepository.SaveAsync();

                //创建成功放回EmployeeDto结果给前台
                var dto = _mapper.Map<EmployeeDto>(employaddentity);
                return CreatedAtRoute(nameof(GetEmployeeForCanpany), new { companyid = companyId, empolyid = dto.Id }, dto);//调用getcompany方达，填写路由

            }
            //把employentity转为EmployeeUpdateDto
            //把传入的employeeUpdateDto替换到把employentity转为EmployeeUpdateDto的中
            //再把EmployeeUpdateDto转为employentity返回

            var Emloyee = _mapper.Map(employeeUpdateDto, employentity);
            _companyRepository.UpdataEmloyee(employentity);//可以不写
            await _companyRepository.SaveAsync();

            return NoContent();
        }
        /*
         	{
		"op":"replace",
		"path":"/employeeNo",
		"value":"1111111111"
	}
         */


        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
            Guid companyId,
            Guid employeeId,
            JsonPatchDocument<EmployeeUpdateDto> jsonPatchDocument)
        {
            if (!await _companyRepository.CompanyExitsAsync(companyId))
            {
                return NotFound();
            }
            var employentity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employentity == null)
            {
                var employeeedto = new EmployeeUpdateDto();
                jsonPatchDocument.ApplyTo(employeeedto, ModelState);
                if(!TryValidateModel(employeeedto))
                {
                    return ValidationProblem(ModelState);
                }

                var employeeToadd = _mapper.Map<Emloyee>(employeeedto);
                employeeToadd.Id = employeeId;
                _companyRepository.AddEmployee(employeeToadd, companyId);
                await _companyRepository.SaveAsync();

                var retdto = _mapper.Map<EmployeeDto>(employeeToadd);
                return CreatedAtRoute(nameof(GetEmployeeForCanpany), new { companyid = companyId, empolyid = retdto.Id }, retdto);//调用getcompany方达，填写路由

            }

            var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employentity);
            //需要处理验证
            jsonPatchDocument.ApplyTo(dtoToPatch,ModelState);//第二个参数是验证jsonPatchDocument传入的值是否合法使其不报500服务器错误
            if (! TryValidateModel(dtoToPatch))//验证数据可以进去我们设置的验证
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(dtoToPatch,employentity);
            _companyRepository.UpdataEmloyee(employentity);
            await _companyRepository.SaveAsync();
            return NoContent();

        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExitsAsync(companyId))
            {
                return NotFound();
            }
            var employentity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employentity == null)
            {
                return NotFound();
            }
            _companyRepository.DeleteEmployee(employentity);
            await _companyRepository.SaveAsync();
            return NoContent();

        }




        //引用我们自己的配置startuo中  报422错误
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var ops = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)ops.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
