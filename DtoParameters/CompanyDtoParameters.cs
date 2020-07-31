using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routines.Api.DtoParameters
{
    public class CompanyDtoParameters
    {
        public const int MaxpageSize = 20;
        public string CompanyName { get; set; }

        public string SerchTerm { get; set; }

        public int PageNumber { get; set; } = 1;


        private int _pageSize = 2;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxpageSize) ? MaxpageSize : value; }
        }

        public string OrderBy { get; set; } = "CompanyName";

        public string Fields { get; set; }
    }
}
