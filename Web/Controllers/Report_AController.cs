using Model.EL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Model.EL;
using OfficeOpenXml;

namespace Web.Controllers
{
    public class Report_AController : Controller
    {

        DataContext db = new DataContext();
        // GET: Report_A

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Export()
        {
            var stream = new MemoryStream();
            var data = db.Categories.ToList();
            var fileName = $"Loai_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs
            // sử dụng giấy phép phi thương mại để đăng kí nếu không có sẽ sai
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Loai");
                // đổ dữ liệu vào sheet;
                // đây đổ hết bằng list vừa tạo
                //sheet.Cells.LoadFromCollection(data, true);

                // đổ dữ liệu vào sheet theo cột tự tạo tự cấu hình
                // Tiêu đề mỗi cột
                sheet.Cells[1, 1].Value = "Mã loại";
                sheet.Cells[1, 2].Value = "Tên loại ENGLISH";
                sheet.Cells[1, 3].Value = "Tên loại VN";

                int rowIndex = 2;
                foreach (var l in data)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = l.Id;
                    sheet.Cells[rowIndex, 2].Value = l.Name;
                    sheet.Cells[rowIndex, 3].Value = l.NameVN;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        public ActionResult Tonkho()
        {
            var tonkho = db.Products.GroupBy(p => p.Category).OrderByDescending(p => p.Sum(c => c.UnitPrice * c.Quantity))
                .Select(p => new
                {
                    Group = p.Key.NameVN,
                    Sum = p.Sum(c => c.UnitPrice * c.Quantity),
                    Count = p.Sum(c => c.Quantity),
                    Min = p.Min(c => c.UnitPrice),
                    Max = p.Max(c => c.UnitPrice),
                    Avg = p.Average(c => c.UnitPrice)
                });

            var stream = new MemoryStream();
            var fileName = $"Tonkho_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs
            // sử dụng giấy phép phi thương mại để đăng kí nếu không có sẽ sai
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Loai");
                // đổ dữ liệu vào sheet;
                // đây đổ hết bằng list vừa tạo
                //sheet.Cells.LoadFromCollection(data, true);

                // đổ dữ liệu vào sheet theo cột tự tạo tự cấu hình
                // Tiêu đề mỗi cột
                sheet.Cells[1, 1].Value = "Tên Mặt hàng";
                sheet.Cells[1, 2].Value = "Tổng giá trị";
                sheet.Cells[1, 3].Value = "Số lượng";
                sheet.Cells[1, 4].Value = "Giá cao nhất";
                sheet.Cells[1, 5].Value = "giá thấp nhất";
                sheet.Cells[1, 6].Value = "Giá trung bình";
               
                int rowIndex = 2;
                foreach (var tk in tonkho)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = tk.Group;
                    sheet.Cells[rowIndex, 2].Value = tk.Sum;
                    sheet.Cells[rowIndex, 3].Value = tk.Count;
                    sheet.Cells[rowIndex, 4].Value = tk.Max;
                    sheet.Cells[rowIndex, 5].Value = tk.Min;
                    sheet.Cells[rowIndex, 6].Value = tk.Avg;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        public ActionResult Doanhthu_KH()
        {
            
            var dt = db.Orders.Select(d => new
                    {
                        Group = d.Customer.Fullname,
                        Sum = d.Amount// Tổng số tiền mua
                        
                       

                    });
         


           

            var stream = new MemoryStream();
            var fileName = $"Tonkho_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs
            // sử dụng giấy phép phi thương mại để đăng kí nếu không có sẽ sai
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Loai");
                // đổ dữ liệu vào sheet;
                // đây đổ hết bằng list vừa tạo
                //sheet.Cells.LoadFromCollection(data, true);

                // đổ dữ liệu vào sheet theo cột tự tạo tự cấu hình
                // Tiêu đề mỗi cột
                sheet.Cells[1, 1].Value = "Tên Khách hàng";
                sheet.Cells[1, 2].Value = "Tổng giá trị mua hàng";
               

                int rowIndex = 2;
                foreach (var doanhthu in dt)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = doanhthu.Group;
                    sheet.Cells[rowIndex, 2].Value = doanhthu.Sum;
                  
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

    }
}