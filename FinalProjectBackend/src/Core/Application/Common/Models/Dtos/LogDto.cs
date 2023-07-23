using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.Dtos
{
    public class LogDto
    {
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public LogDto(string message)
        {
            Message = message;
            SentOn = DateTimeOffset.Now;
                
        }
    }
}
