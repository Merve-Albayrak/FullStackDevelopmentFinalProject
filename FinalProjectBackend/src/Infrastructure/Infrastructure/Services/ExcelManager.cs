using Application.Common.Interfaces;
using Domain.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExcelManager : IExcelService
    {
        //excel kısmında product listesini alıp onu excel dosyasına dönüştürüyorum
        public void ExportToExcel(List<Product> products)
        {
            var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var filePath = Path.Combine(downloadsPath, "ProductData.xlsx");
            var newFile = new FileInfo(filePath);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ProductData");

              
                worksheet.Cells[1, 1].Value = "Order Id";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Picture";
                worksheet.Cells[1, 4].Value = "Is On Sale";
                worksheet.Cells[1, 5].Value = "Price";
                worksheet.Cells[1, 6].Value = "Sale Price";

              
                int row = 2;
                foreach (var product in products)
                {
                    worksheet.Cells[row, 1].Value = product.OrderId;
                    worksheet.Cells[row, 2].Value = product.Name;
                    worksheet.Cells[row, 3].Value = product.Picture;
                    worksheet.Cells[row, 4].Value = product.IsOnSale;
                    worksheet.Cells[row, 5].Value = product.Price;
                    worksheet.Cells[row, 6].Value = product.SalePrice;
                    row++;
                }


            }
        }
    }
}
