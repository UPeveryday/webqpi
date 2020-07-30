using Routines.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Routines.Api.Models
{
    public class CompanyAddDto
    {
        //此添加公司dto可能和请求dto相同但是建议使用两个dto
        [Display(Name ="公司名称")]
        [Required(ErrorMessage ="{0}名称为必填的信息")]
        [MaxLength(100,ErrorMessage ="{0}的最大长度不可超过{1}")]
        public string Name { get; set; }


        [Display(Name = "简介")]
        [StringLength(500,MinimumLength =10, ErrorMessage = "{0}的长度范围在{2}到{1}")]
        public string Introduction { get; set; }

        public ICollection<EmployeeAddDto> Emloyees { get; set; } = new List<EmployeeAddDto>();
    }
}
