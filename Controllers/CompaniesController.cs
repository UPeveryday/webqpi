using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routines.Api.DtoParameters;
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
    [Route("api/companies")]//和下面一样
    //[Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        //[HttpGet("api/companies")]
        [HttpGet]//类上有route可以省略
        [HttpHead]//支持head
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies([FromQuery] CompanyDtoParameters companyDtoParameters)
        {
            //指定[FromQuery]可自动解析

            //对异常处理 不可这样做
            // throw new Exception("An Exception");

            //get 请求数据所以需要Dto

            //ActionResult<List<CompanyDto>> 在此可以替代IActionResult
            var companiess = await _companyRepository.GetCompaniesAsync(companyDtoParameters);

            var companiessdto = _mapper.Map<IEnumerable<CompanyDto>>(companiess);

            return Ok(companiessdto);
        }

        // [HttpGet("api/companies/{companyid}")]//api/companiees/{companyid}
        [HttpGet("{companyid}", Name = nameof(GetCompany))]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyid)
        {
            //var exit = await _companyRepository.CompanyExitsAsync(companyid);
            //if (!exit)
            //    return NotFound();


            var companiess = await _companyRepository.GetCompanyAsync(companyid);
            if (companiess == null)
                return NotFound();
            return Ok(_mapper.Map<CompanyDto>(companiess));
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyAddDto companyAddDto)
        {
            //post 把是参数把是参数dto转为companydto转为company存入数据库
            //Id 应该在api生成
            //老版本的需要检查 新版本apicontroller会自动返回badrequest
            if (companyAddDto == null)
            {
                return BadRequest();
            }
            var entity = _mapper.Map<Company>(companyAddDto);

            _companyRepository.AddCompany(entity);

            var succss = await _companyRepository.SaveAsync();

            var dto = _mapper.Map<CompanyDto>(entity);

            return CreatedAtRoute(nameof(GetCompany), new { companyid = dto.Id }, dto);//调用getcompany方达，填写路由
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyentity = await _companyRepository.GetCompanyAsync(companyId);
            if (null==companyentity)
            {
                return NotFound();
            }
            await _companyRepository.GetEmployeesAsync(companyId, null, null);
            _companyRepository.DeleteCompany(companyentity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }


        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

    }
}
