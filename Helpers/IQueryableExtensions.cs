using Routines.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Routines.Api.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source,
            string orderby,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (mappingDictionary == null) throw new ArgumentNullException(nameof(mappingDictionary));
            if (string.IsNullOrWhiteSpace(orderby)) return source;

            var orderByAfterSplit = orderby.Split(",");

            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var orderByClausetrim = orderByClause.Trim();
                var orderDescending = orderByClausetrim.EndsWith(" desc");
                var indexOfFirstSpace = orderByClausetrim.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? orderByClausetrim : orderByClausetrim.Remove(indexOfFirstSpace);
                if (!mappingDictionary.ContainsKey(propertyName)) throw new ArgumentNullException(nameof(propertyName));

                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null) throw new ArgumentNullException(nameof(propertyMappingValue));

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    source = source.OrderBy(destinationProperty +
                        (orderDescending ? " descending" : " ascending"));
                }
            }
            return source;
        }
    }
}
