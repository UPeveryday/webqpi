using Microsoft.AspNetCore.Mvc;
using Routines.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Controllers
{
    [Route("api")]
    public class RootController:ControllerBase
    {
        [HttpGet(Name =nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();
            links.Add(new LinkDto(Url.Link(nameof(GetRoot),new { }),"self","GET"));
            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.GetCompanies),new { }), "GetCompanies", "GET"));
            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.CreateCompany),new { }), "CreateCompany", "POST"));
            return Ok(links);
        }
    }
}
