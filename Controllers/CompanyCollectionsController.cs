using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routines.Api.Entities;
using Routines.Api.Helpers;
using Routines.Api.Models;
using Routines.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Controllers
{
    [ApiController]
    [Route("api/companycollections")]
    public class CompanyCollectionsController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyCollectionsController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = nameof(GetComoanyCollections))]
        public async Task<IActionResult> GetComoanyCollections([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            //[FromRoute] 默认是frombody
            if (ids == null)
                return BadRequest();
            var entities = await _companyRepository.GetCompaniesAsync(ids);

            if (ids.Count() != entities.Count())
                return NotFound();

            var dto = _mapper.Map<IEnumerable<CompanyDto>>(entities);

            return Ok(dto);

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyColletions(IEnumerable<CompanyAddDto> companyAddDtos)
        {
            var enity = _mapper.Map<IEnumerable<Company>>(companyAddDtos);
            foreach (var item in enity)
            {
                _companyRepository.AddCompany(item);
            }
            await _companyRepository.SaveAsync();

            var dto = _mapper.Map<IEnumerable<CompanyDto>>(enity);

            var idsstring = string.Join(",", dto.Select(x => x.Id));
            //返回201 create
            return CreatedAtRoute(nameof(GetComoanyCollections), new { ids = idsstring }, dto);//调用的参数

        }
    }
}
