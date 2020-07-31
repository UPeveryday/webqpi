using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.DtoParameters
{
    public class EmployeeParameters
    {
        private const int MaxpageSize = 20;
        public string Gender { get; set; }
        public string Q { get; set; }
        public int pageNumber { get; set; } = 1;
        private int _pageSize=5;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxpageSize) ? MaxpageSize : value; }
        }

        public string OrderBy { get; set; } = "Name";

    }
}
