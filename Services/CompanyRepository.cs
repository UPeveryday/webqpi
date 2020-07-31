using Routines.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routines.Api.Data;
using System.Runtime.CompilerServices;
using Routines.Api.DtoParameters;
using Routines.Api.Helpers;
using Routines.Api.Models;

namespace Routines.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public CompanyRepository(RoutineDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }
        public void AddCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
            company.Id = Guid.NewGuid();
            if (company.Emloyees != null)
            {
                foreach (var employee in company.Emloyees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }
            _context.Companies.Add(company);
        }

        public void AddEmployee(Emloyee emloyee, Guid comanuId)
        {
            if (emloyee == null) throw new ArgumentNullException(nameof(emloyee));
            if (comanuId == Guid.Empty) throw new ArgumentNullException(nameof(comanuId));
            emloyee.CompanyId = comanuId;
            _context.Employees.Add(emloyee);
        }

        public async Task<bool> CompanyExitsAsync(Guid companyID)
        {
            if (companyID == Guid.Empty) throw new ArgumentNullException(nameof(companyID));
            return await _context.Companies.AnyAsync(c => c.Id == companyID);
        }

        public void DeleteCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
            _context.Companies.Remove(company);
        }

        public void DeleteEmployee(Emloyee emloyee)
        {
            if (emloyee == null) throw new ArgumentNullException(nameof(emloyee));
            _context.Employees.Remove(emloyee);
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters companyDtoParameters)
        {
            if (companyDtoParameters == null) throw new ArgumentNullException(nameof(companyDtoParameters));
            //if(string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName)&&string.IsNullOrWhiteSpace(companyDtoParameters.SerchTerm))
            //{
            //    return await _context.Companies.ToListAsync();
            //}

            var queryExpress = _context.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName))
            {
                companyDtoParameters.CompanyName = companyDtoParameters.CompanyName.Trim();
                queryExpress = queryExpress.Where(x => x.Name == companyDtoParameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace(companyDtoParameters.SerchTerm))
            {
                companyDtoParameters.SerchTerm = companyDtoParameters.SerchTerm.Trim();
                queryExpress = queryExpress.Where(x => x.Name.Contains(companyDtoParameters.SerchTerm) || x.Introduction.Contains(companyDtoParameters.SerchTerm));
            }
            //翻页逻辑在查询之后
            //   queryExpress = queryExpress.Skip(companyDtoParameters.PageSize * (companyDtoParameters.PageNumber - 1)).Take(companyDtoParameters.PageSize);
            //return await queryExpress.OrderBy(x => x.Name).ToListAsync();

            //排序
            var mappingDictionary = _propertyMappingService.GetPropertyMapping<CompanyDto, Company>();
            queryExpress = queryExpress.ApplySort(companyDtoParameters.OrderBy, mappingDictionary);

            //利用自定义PagedList创建分页
            return await PagedList<Company>.Create(queryExpress, companyDtoParameters.PageNumber, companyDtoParameters.PageSize);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null) throw new ArgumentNullException(nameof(companyIds));
            return await _context.Companies.Where(c => companyIds.Contains(c.Id)).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyid)
        {
            if (companyid == Guid.Empty) throw new ArgumentNullException(nameof(companyid));
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyid);
        }

        public async Task<Emloyee> GetEmployeeAsync(Guid companyid, Guid employeeid)
        {
            if (companyid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyid));
            }

            if (employeeid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeid));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyid && x.Id == employeeid)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Emloyee>> GetEmployeesAsync(Guid companyid, EmployeeParameters parameters)
        {

            if (companyid == null) throw new ArgumentNullException(nameof(companyid));
            //if (string.IsNullOrWhiteSpace(parameters.Gender) && string.IsNullOrWhiteSpace(parameters.Q))
            //{
            //    return await _context.Employees.Where(x => x.CompanyId == companyid).OrderBy(x => x.EmployeeNo)
            //.ToListAsync();
            //}
            var items = _context.Employees.Where(x => x.CompanyId == companyid);
            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                var gender = Enum.Parse<Gender>(parameters.Gender.Trim());
                items = items.Where(x => x.Gender == gender);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Q))
            {
                parameters.Q = parameters.Q.Trim();
                items = items.Where(x => x.EmployeeNo.Contains(parameters.Q) || x.FirstName.Contains(parameters.Q) || x.LastName.Contains(parameters.Q));
            }

            //过滤搜索结束开始排序  自定义映射关系
            var mappingDictionay = _propertyMappingService.GetPropertyMapping<EmployeeDto, Emloyee>();
            items = items.ApplySort(parameters.OrderBy, mappingDictionay);

            return await items.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdataCompany(Company company)
        {
            //  throw new NotImplementedException();
        }

        public void UpdataEmloyee(Emloyee emloyee)
        {
            //  throw new NotImplementedException();
        }
    }
}
