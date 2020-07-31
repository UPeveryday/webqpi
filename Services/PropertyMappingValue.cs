using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; set; }//排序
        public PropertyMappingValue(IEnumerable<string> eestinationProperties, bool revert=false)
        {
            DestinationProperties = eestinationProperties ?? throw new ArgumentNullException(nameof(eestinationProperties));
            Revert = revert;
        }
    }
}
