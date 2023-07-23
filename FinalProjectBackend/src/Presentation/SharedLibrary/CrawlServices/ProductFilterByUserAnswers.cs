using Crawler.Models;

using OpenQA.Selenium;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    //BU CLASS USER CEVABINA GÖRE PARÇALAMA SINIFINA İŞLEM YAPTIRIYOR
    public class ProductFilterByUserAnswers
    {
        private Crawl _crawler;
      

        public ProductFilterByUserAnswers(string baseUrl,IWebDriver webDriver) {

            UserAnswers = new Dictionary<string, string>();

            _crawler = new Crawl(baseUrl, webDriver);
        } 


        //USERDAN CEVAPLARI ALIYOR
        public Dictionary<string, string> UserAnswers { get; }

        public void GetUserAnswers(string selectProductType, string allOrCount, int productCount)
        {
            if (!UserAnswers.ContainsKey("ProductType"))
            {
                UserAnswers.Add("ProductType", selectProductType);
            }
            else
            {
                UserAnswers["ProductType"] = selectProductType;

            }
            if (!UserAnswers.ContainsKey("AllOrCount"))
            {

                UserAnswers.Add("AllOrCount", allOrCount);
            }
            else
            {

                UserAnswers["AllOrCount"]=allOrCount;
            }
            if (!UserAnswers.ContainsKey("ProductCount"))
            {

                UserAnswers.Add("ProductCount", productCount.ToString());
            }
            else
            {

                UserAnswers["ProductCount"]=productCount.ToString();
            }
           
            
        }
        //PRODUCTLARI VERİLEN FİLTRELERE GÖRE CRAWL SINIFINOI KULLANARAK BULUYOR VE DÖNDÜRÜYOR
        public List<Product> FilteredProducts()
        {
            string answerProductType;
        UserAnswers.TryGetValue("ProductType", out answerProductType);

            string answerAllOrCount;
            UserAnswers.TryGetValue("AllOrCount",out  answerAllOrCount);

            string answerProductCount;
            UserAnswers.TryGetValue("ProductCount", out answerProductCount);
            List<Product> filteredProducts = new List<Product>();
            if (answerAllOrCount == "All")
            {


                filteredProducts = GetAllProducts(answerProductType);
            }

          if(answerAllOrCount == "Enter the Number")
            {


                filteredProducts
                     = GetProductsByCount(Convert.ToInt32(answerProductCount), answerProductType);
               if (filteredProducts.Count % Convert.ToInt32(answerProductCount)== 0&&filteredProducts.Count!=0 )
               {

                   return filteredProducts;


               }

               else
               {


                   int differenceCount = filteredProducts.Count - Convert.ToInt32(answerProductCount);
                   filteredProducts.RemoveRange(filteredProducts.Count-1-differenceCount, differenceCount);
               }
            }

          return filteredProducts;
         

        }
        //SAYIYLA PRODUCT İSTEMİŞSE ÜSTTEKİ METOD BUNU KULLANIYOR
        public List<Product> GetProductsByCount(int count,string productType)
        {


            var products =_crawler.FindProductsFromAllPagesByCount(count, productType);
            return products;
        }
        //TÜMÜ İSTENMİŞSE BU METOD DEVREYE GİRİYOR
        public List<Product> GetAllProducts( string productType)
        {


            var products = _crawler.FindProductsFromAllPages(productType);
            return products;
        }
     
    }
}
