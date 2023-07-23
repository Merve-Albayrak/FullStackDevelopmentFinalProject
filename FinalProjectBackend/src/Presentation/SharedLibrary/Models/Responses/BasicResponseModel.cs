using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Models.Responses
{
    public class BasicResponseModel
    {


        public string message { get; set; } 
        public string data { get; set; } 
        public List<string> errors { get; set; }

    }
}
