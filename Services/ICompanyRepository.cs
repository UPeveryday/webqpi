﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routines.Api.DtoParameters;
using Routines.Api.Entities;
using Routines.Api.Helpers;

namespace Routines.Api.Services
{
    public interface ICompanyRepository
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters companyDtoParameters);
        Task<Company> GetCompanyAsync(Guid companyid);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);

        void AddCompany(Company company);
        void UpdataCompany(Company company);
        void DeleteCompany(Company company);

        Task<bool> CompanyExitsAsync(Guid companyID);

        Task<IEnumerable<Emloyee>> GetEmployeesAsync(Guid companyid,EmployeeParameters parameters);
        Task<Emloyee> GetEmployeeAsync(Guid companyid, Guid employeeid);

        void AddEmployee(Emloyee emloyee,Guid comanuId);
        void UpdataEmloyee(Emloyee emloyee);
        void DeleteEmployee(Emloyee emloyee);

        Task<bool> SaveAsync();


    }
}
