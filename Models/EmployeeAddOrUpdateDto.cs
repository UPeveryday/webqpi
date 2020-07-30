using Routines.Api.Entities;
using Routines.Api.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Models
{
    [EmploeeNoMustDifferentFromFirstName(ErrorMessage = "员工编号不可以和名字一样 Validation")]
    public class EmployeeAddOrUpdateDto : IValidatableObject
    {
        //  public Guid CompanyId { get; set; }//添加一个已经知道的公司下面需要输入companyguid 所以在此不需要呀
        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}为必填的信息")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}的最大长度不可超过{1}")]
        public string EmployeeNo { get; set; }
        [Display(Name = " 名")]
        [Required(ErrorMessage = "{0}为必填的信息")]
        [MaxLength(10, ErrorMessage = "{0}的最大长度不可超过{1}")]
        public string FirstName { get; set; }
        [Display(Name = " 名")]
        [Required(ErrorMessage = "{0}为必填的信息")]
        [MaxLength(10, ErrorMessage = "{0}的最大长度不可超过{1}")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = " 出生日期")]
        public DateTime DateOfBirth { get; set; }

        //如果 attribute验证合法的时候才会进入自定义验证  
        //跨属性验证
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                //  yield return new ValidationResult("姓和名不可重复",new[] { nameof(EmployeeAddDto) });
                yield return new ValidationResult("姓和名不可重复", new[] { nameof(FirstName), nameof(LastName) });
            }
        }
    }
}
