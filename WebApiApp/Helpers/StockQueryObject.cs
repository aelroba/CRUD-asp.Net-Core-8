using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.Helpers
{
    public class StockQueryObject
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public string? SortBy { get; set; }
        public bool IsDec { get; set; } = true;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set;} = 1;
    }
}