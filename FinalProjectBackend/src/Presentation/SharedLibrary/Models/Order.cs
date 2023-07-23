using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Order : EntityBase<Guid>
    {


      //  public OrderStatus orderStatus { get; set; }
        public int expectedCount { get; set; }
        public DateTimeOffset startingDate { get; set; }
        public DateTimeOffset finishingDate { get; set; }

        //   public int FoundedProductCount { get; set; }

      
       
    


        public int? foundedCount { get; set; }

     
        public Guid? orderEventId { get; set; }
        public Guid? userId { get; set; }


        //  public Guid ProductId { get; set; }

        public bool? isFinished { get; set; }


    }
}  
