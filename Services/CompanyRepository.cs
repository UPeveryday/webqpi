using Routines.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routines.Api.Data;
using System.Runtime.CompilerServices;
using Routines.Api.DtoParameters;

namespace Routines.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;

        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddCompany(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));
            company.Id = Guid.NewGuid();
            if(company.Emloyees!=null)
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

        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters companyDtoParameters)
        {
            if (companyDtoParameters == null) throw new ArgumentNullException(nameof(companyDtoParameters));
            if(string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName)&&string.IsNullOrWhiteSpace(companyDtoParameters.SerchTerm))
            {
                return await _context.Companies.ToListAsync();
            }

            var queryExpress = _context.Companies as IQueryable<Company>;
            if(!string.IsNullOrWhiteSpace(companyDtoParameters.CompanyName))
            {
                companyDtoParameters.CompanyName = companyDtoParameters.CompanyName.Trim();
                queryExpress = queryExpress.Where(x => x.Name == companyDtoParameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace(companyDtoParameters.SerchTerm))
            {
                companyDtoParameters.SerchTerm = companyDtoParameters.SerchTerm.Trim();
                queryExpress = queryExpress.Where(x => x.Name.Contains(companyDtoParameters.SerchTerm)||x.Introduction.Contains(companyDtoParameters.SerchTerm));
            }
            return await queryExpress.OrderBy(x => x.Name).ToListAsync();
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

        public async Task<IEnumerable<Emloyee>> GetEmployeesAsync(Guid companyid, string genderDisplay,string q)
        {

            if (companyid == null) throw new ArgumentNullException(nameof(companyid));
            if (string.IsNullOrWhiteSpace(genderDisplay)&&string.IsNullOrWhiteSpace(q))
            {
                return await _context.Employees.Where(x => x.CompanyId == companyid).OrderBy(x => x.EmployeeNo)
            .ToListAsync();
            }

            var items=_context.Employees.Where(x=>x.CompanyId==companyid);
            if (!string.IsNullOrWhiteSpace(genderDisplay) )
            {
                var gender = Enum.Parse<Gender>(genderDisplay.Trim());
                items = items.Where(x => x.Gender == gender);
            }
            if(!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                items = items.Where(x => x.EmployeeNo.Contains(q)||x.FirstName.Contains(q)||x.LastName.Contains(q));
            }
            return await items.OrderBy(x => x.EmployeeNo) .ToListAsync();
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
