using Domain.Common;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.WorkerService
{
    public class WorkerServiceNewOrderAddedDto
    {
        private Guid data;

        //public Response<Guid> Data { get; set; }
        public string AccessToken { get; set; }
        //public WorkerServiceNewOrderAddedDto(Domain.Common.Response<Guid> data,string accessToken)
        //{
        //    Data=data;
        //    AccessToken=accessToken;
            
        //}

        public WorkerServiceNewOrderAddedDto(Guid data, string accessToken)
        {
            this.data = data;
            AccessToken = accessToken;
        }
    }
}
