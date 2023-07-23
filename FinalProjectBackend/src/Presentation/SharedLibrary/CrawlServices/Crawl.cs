using Crawler.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
//using DotNetSeleniumExtras;
namespace SharedLibrary
{
    //BURASI BENİM ASIL KAZIMA İŞLEMLERİNİ YAPAN SINIFIM
    public class Crawl
    {
        public string BaseUrl { get; set; }
        public IWebDriver WebDriver { get; set; }
        public Crawl( string url,IWebDriver webDriver)
        {
            BaseUrl = url;
        
           WebDriver = webDriver;
            WebDriver = new ChromeDriver();


            
        }

        //BİR SAYFADAKİ TÜM ÜRÜNLERİ TESPİT EDER
        public  List<IWebElement> FindElementsFromSinglePage(string xpath,string url) {
          //  WebDriver =new ChromeDriver();
            WebDriver.Navigate().GoToUrl(url);
            List<IWebElement> elements = new List<IWebElement>();
        
                 
                    elements.AddRange(WebDriver.FindElements(By.CssSelector(xpath)).ToList());

    
            return elements;
        
        
        }
        //BU METOD TESPİT EDİLEN ÜRÜNLERİ PRODUCT CLASSINA ÇEVİRMEKLE GÖREVLİ
        public List<Product> FindProductsEachPage(string url,string productType) {

            List<IWebElement> elements = new List<IWebElement>();
        //    url = BaseUrl;
            List<Product> products = new List<Product>();

        
           
            elements = FindElementsFromSinglePage("div[class='card h-100']", url);
            foreach (IWebElement element in elements)
            {
                Product product = new Product();

                var elementProductName = element.FindElement(By.CssSelector("h5[class='fw-bolder product-name']"));
                product.name = elementProductName.Text;

                IWebElement pictureElement;
                pictureElement  = element.FindElement(By.CssSelector("[class='card-img-top']"));
                string pictureUrl = pictureElement.GetAttribute("src").ToString();

                product.picture=pictureUrl;

              //  string folderPath = AppDomain.CurrentDomain.BaseDirectory;

              //  DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
              //string root=    Directory.GetDirectoryRoot(folderPath);

              //  // Eğer klasör yoksa oluştur
              //  if (!Directory.Exists(folderPath))
              //  {
              //      Directory.CreateDirectory(folderPath);
              //  }

              //  using (WebClient client = new WebClient())
              //  {
              //      string fileName = product.name + ".jpg";
              //     // string filePath = Path.Combine("..\", fileName);
              //     // client.DownloadFile(pictureUrl, filePath);
              //     // Console.WriteLine("Resim kaydedildi: " + filePath);
              //  }
                //product.picture=
                IWebElement elementProductPrice;
                IWebElement salePrice;
 
                if (IsElementPresent(element,By.CssSelector("span[class='price']"))){
                 
                    
                    elementProductPrice = element.FindElement(By.ClassName("price"));
                    product.price = Convert.ToDecimal(elementProductPrice.Text.ToString().Replace("$", ""));
                    product.isOnSale = false;
                 //   salePrice = elementProductPrice;
                    

                }

                if (IsElementPresent(element,By.CssSelector("span[class='text-muted text-decoration-line-through price']")))

                {

                    elementProductPrice = element.FindElement(By.ClassName("text-muted"));
                    product.price =Convert.ToDecimal( elementProductPrice.Text.ToString().Replace("$",""));
                    salePrice = element.FindElement(By.ClassName("sale-price"));

                  //  var salePrice = product.salePrice.ToString().Substring(product.salePrice.ToString().IndexOf("$")); 
                  product.salePrice=Convert.ToDecimal(salePrice.Text.ToString().Replace("$",""));
                 //   product.salePrice = Convert.ToDecimal(salePrice.Text);
                    product.isOnSale = true;
                }


             if(productType=="All")
                {


                    products.Add(product);

                }
             if(productType=="On Sale" && product.isOnSale == true)
                {


                    products.Add(product);
                }
             if(productType=="Regular Prices"&&product.isOnSale == false)
                {

                    products.Add(product);

                }




            }
             return products;

        }
        //TÜM SAYFALARDAKİ TÜM ÜRÜNLERİ PARÇALAYIP CLASSA DÖNÜŞTÜRÜYOR
        //BUNU TÜM ÜRÜNLERİ GETİRMEDE KULLANIYORUM
        public List<Product> FindProductsFromAllPages(string productType)
        {
            
            List<Product> products = new List<Product>();

        
            products.AddRange(FindProductsEachPage(BaseUrl, productType));
            int totalPage = FindCountOfPages();
           

            for (int i = 2; i < totalPage; i++)
            { 

                string url = BaseUrl;
                url = BaseUrl + "/?currentPage=" + i;
                products.AddRange(FindProductsEachPage(url,productType));



                WebDriver.Navigate().GoToUrl(url);
              
              
             
            }
            return products;


        }
        //İSTENİLEN ÜRÜN ADETİNE GÖRE SAYFA DEĞİŞTİRİP ÜRÜNLERİ ALIYOR
        //ÜSTTEKİ İKİ METODU ÇAĞIRARAK BU İŞLEMİ YAPIYOR
        public List<Product> FindProductsFromAllPagesByCount(int count, string productType)
        {
            List<Product> products = new List<Product>();

            products.AddRange(FindProductsEachPage(BaseUrl, productType));
            int totalPage = FindCountOfPages();
            if (products.Count >= count)
            {


                return products;
            }

            for (int i = 2; i < totalPage; i++)
            {
                string url = BaseUrl;
                url = BaseUrl + "/?currentPage=" + i;
                products.AddRange(FindProductsEachPage(url, productType));


                if (products.Count >= count)
                {


                    return products;
                }

                WebDriver.Navigate().GoToUrl(url);


            }
            return products;


        }
        //KAÇ TANE SAYFA VAR BUNU BULUYOR
        public int FindCountOfPages() 
        {


            int pageCount = WebDriver.FindElements(By.XPath("/html/body/section/div/nav/ul/li")).Count;

            return pageCount;
        }

       
       

       
       
        //BİR SAYFADA VERİLENT ELEMENT VAR MI BUNU TESPİT EDİYOR
        private bool IsElementPresent(IWebElement element,By by)
        {
            try
            {
              //  WebDriver.FindElement(by);
                element.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
      

    }
}
