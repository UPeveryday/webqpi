using Routines.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Models
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string GenderDisPlay { get; set; }
        public int Age { get; set; }
    //    public CompanyDto CompanyDto { get; set; }
    }
}
