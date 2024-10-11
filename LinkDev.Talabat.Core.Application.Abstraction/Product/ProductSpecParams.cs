using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Product
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private int pageSize = 5;
        private const int PageSizeMax = 10; 
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > PageSizeMax? PageSizeMax : value ; }
        }

        private int pageIndex = 1;
        public int PageIndex
        {
            get { return pageIndex ; }
            set { pageIndex = value; }
        }


    }
}
