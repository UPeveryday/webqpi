using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Services
{
    public interface IPropertyMappingService
    {
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        public bool ValidMappingExistsFor<TSource, TDestination>(string fileds);
    }
}
