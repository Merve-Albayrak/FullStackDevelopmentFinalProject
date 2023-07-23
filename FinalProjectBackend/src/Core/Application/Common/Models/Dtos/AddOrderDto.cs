using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.Dtos
{
    public class AddOrderDto
    {
        public string Count { get; set; }
        public string ProductType { get; set; }
        public string AllOrCount { get; set; }

        public AddOrderDto(string allOrCount, string productType,string count)
        {
            AllOrCount = allOrCount;
            ProductType = productType;
            Count = count;


        }
    }
}
