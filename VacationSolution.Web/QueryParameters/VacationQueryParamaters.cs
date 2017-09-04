using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationSolution.Web.QueryParameters
{
    public class VacationQueryParamaters
    {
        private const int MaxPageCount = 100;
        public int Page { get; set; } = 1;

        private int _PageCount = 100;

        public int PageCount
        {
            get { return _PageCount; }
            set { _PageCount = (value > MaxPageCount ? MaxPageCount : value); }
        }

        //Adding Query
        public bool HasQuery { get { return !string.IsNullOrEmpty(Query); } }

        public string Query { get; set; }

        //Adding Sorting param

        public string OrderBy { get; set; } = "StartDate";

        public bool Descending
        {
            get
            {
                if (!string.IsNullOrEmpty(OrderBy))
                {
                    return OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
                }
                return false;
            }
        }
    }
}
