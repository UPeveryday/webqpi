using AutoMapper;
using Routines.Api.Entities;
using Routines.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Emloyee, EmployeeDto>().ForMember(desk => desk.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).ForMember(desk =>
            desk.GenderDisPlay, opt => opt.MapFrom(src => src.Gender.ToString())).ForMember(desk => desk.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<EmployeeAddDto, Emloyee>();
            CreateMap<EmployeeUpdateDto, Emloyee>();
            CreateMap< Emloyee, EmployeeUpdateDto>();
        }
    }
}
