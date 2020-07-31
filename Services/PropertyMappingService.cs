using Routines.Api.Entities;
using Routines.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Services
{
    public class PropertyMappingService:IPropertyMappingService
    {
        //  Employee到EmployeeDto的映射
        private Dictionary<string, PropertyMappingValue> _employPropertyMapping = new Dictionary<string, PropertyMappingValue>()
        {
                 {"Id", new PropertyMappingValue(new List<string>{"Id"}) },
                {"CompanyId", new PropertyMappingValue(new List<string>{"CompanyId"}) },
                {"EmployeeNo", new PropertyMappingValue(new List<string>{"EmployeeNo"}) },
                {"Name", new PropertyMappingValue(new List<string>{"FirstName", "LastName"})},
                {"GenderDisPlay", new PropertyMappingValue(new List<string>{"Gender"})},
                {"Age", new PropertyMappingValue(new List<string>{"DateOfBirth"}, true)}
        };

        private readonly Dictionary<string, PropertyMappingValue> _companyPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
                {"Id", new PropertyMappingValue(new List<string>{"Id"}) },
                {"CompanyName", new PropertyMappingValue(new List<string>{"Name"}) },
                {"Country", new PropertyMappingValue(new List<string>{"Country"}) },
                {"Industry", new PropertyMappingValue(new List<string>{ "Industry"})},
                {"Product", new PropertyMappingValue(new List<string>{"Product"})},
                {"Introduction", new PropertyMappingValue(new List<string>{"Introduction"})}
          };
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Emloyee>(_employPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(_companyPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var macthMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (macthMapping.Count() == 1)
            {
                return macthMapping.First().MappingDictionnary;
            }
            throw new Exception($"无法找到 唯一的映射关系：{typeof(TSource)},{typeof(TDestination)}");
        }

        //处理返回500代码问题 应该是返回4开头错误码
        public bool ValidMappingExistsFor<TSource,TDestination>(string fileds)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();
            if (string.IsNullOrWhiteSpace(fileds)) return true;
            var filedafterSpilite= fileds.Split(",");
            foreach (var filed in filedafterSpilite)
            {
                var trimmedFileds = filed.Trim();
                var indexOfFirst = trimmedFileds.IndexOf(" ");
                var propertyName = indexOfFirst == -1 ? trimmedFileds : trimmedFileds.Remove(indexOfFirst);
                if (!propertyMapping.ContainsKey(propertyName)) return false;
            }

            return true;
        }

    }
}
