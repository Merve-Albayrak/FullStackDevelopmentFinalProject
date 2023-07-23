using Crawler.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    //bu sınıf response dan dönen cevapları parçalayıp basicresponse modele ayırıyor
    //böyle hem data hem status kısımlarını ayrı ayrı kullanıyorum
    //yine her istekte kod tekrarına düşmemek için metod yazdım
    public class ConvertHttpMessages
    {
        public   async Task<BasicResponseModel> ConvertHttpMessagesToBasicResponseModel(HttpResponseMessage? message)
        {


            string responseData = await message.Content.ReadAsStringAsync();
            BasicResponseModel responseModel = new BasicResponseModel();
            responseModel=  JsonConvert.DeserializeObject<BasicResponseModel>(  responseData);
            return responseModel;



        }

    }
}
