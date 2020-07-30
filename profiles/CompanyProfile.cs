using AutoMapper;
using Routines.Api.Entities;
using Routines.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.profiles
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {
            //CreateMap<Company, CompanyDto>();
            CreateMap<Company, CompanyDto>().ForMember(dest=>dest.CompanyName,opt=>opt.MapFrom(src=>src.Name));
            CreateMap<CompanyAddDto, Company>();
            //如果映射名称不同这里可以指定名称不同的
        }
    }
}
