using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EL;
using OfficeOpenXml;

using System.IO;

using Model.Dao;
using Web.ReportServices;

namespace Web.Areas.Admin.Controllers
{
    public class ReportAController : Controller
    {
        // GET: Admin/ReportA
        DataContext db = new DataContext();
        ReportDAO report_dao = new ReportDAO();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Export_Tonkho()
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

        public ActionResult Export_Doanhthu_KH()
        {
            DataContext db = new DataContext();

            var dt = db.OrderDetails.GroupBy(o => o.Order.Customer).Select(o => new
            {
                group = o.Key.Fullname,
                sum = o.Sum(p => p.UnitPrice * p.Quantity),
                count = o.Sum(p => p.Quantity),
                min = o.Min(p => p.UnitPrice),
                max = o.Max(p => p.UnitPrice),
                avg = o.Average(p => p.UnitPrice)



            });

            var stream = new MemoryStream();
            var fileName = $"Doanhthu_KH_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs


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
                sheet.Cells[1, 1].Value = "Tên khách hàng";
                sheet.Cells[1, 2].Value = "Tổng giá trị";
                sheet.Cells[1, 3].Value = "Số lượng";
                sheet.Cells[1, 4].Value = " Giá trị mặt hàng lớn nhất";
                sheet.Cells[1, 5].Value = " Giá trị mặt hàng nhỏ nhất";
                sheet.Cells[1, 6].Value = "Giá  trị mặt hàng trung bình";

                int rowIndex = 2;
                foreach (var tk in dt)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = tk.group;
                    sheet.Cells[rowIndex, 2].Value = tk.sum;
                    sheet.Cells[rowIndex, 3].Value = tk.count;
                    sheet.Cells[rowIndex, 4].Value = tk.max;
                    sheet.Cells[rowIndex, 5].Value = tk.min;
                    sheet.Cells[rowIndex, 6].Value = tk.avg;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        public ActionResult Export_Doanhthu_Nam()
        {
            DataContext db = new DataContext();
            var year = db.OrderDetails.GroupBy(o => o.Order.OrderDate.Year)
                        .Select(o => new
                        {

                            group = o.Key,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity),

                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });

            var stream = new MemoryStream();
            var fileName = $"Doanhthu_Nam_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs


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
                sheet.Cells[1, 1].Value = "Năm";
                sheet.Cells[1, 2].Value = "Tổng giá trị";
                sheet.Cells[1, 3].Value = "Số lượng";
                sheet.Cells[1, 4].Value = " Giá trị mặt hàng lớn nhất";
                sheet.Cells[1, 5].Value = " Giá trị mặt hàng nhỏ nhất";
                sheet.Cells[1, 6].Value = "Giá  trị mặt hàng trung bình";

                int rowIndex = 2;
                foreach (var tk in year)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = tk.group;
                    sheet.Cells[rowIndex, 2].Value = tk.sum;
                    sheet.Cells[rowIndex, 3].Value = tk.count;
                    sheet.Cells[rowIndex, 4].Value = tk.max;
                    sheet.Cells[rowIndex, 5].Value = tk.min;
                    sheet.Cells[rowIndex, 6].Value = tk.avg;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        public ActionResult Export_Doanhthu_Thang()
        {
            //List<Report> r = new List<Report>();
            DataContext db = new DataContext();
            var month = db.OrderDetails.GroupBy(o => o.Order.OrderDate.Month)
                        .Select(o => new
                        {

                            group = o.Key,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity),

                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });

            var stream = new MemoryStream();
            var fileName = $"Doanhthu_Thang_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs


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
                sheet.Cells[1, 1].Value = "Tháng";
                sheet.Cells[1, 2].Value = "Tổng giá trị";
                sheet.Cells[1, 3].Value = "Số lượng";
                sheet.Cells[1, 4].Value = " Giá trị mặt hàng lớn nhất";
                sheet.Cells[1, 5].Value = " Giá trị mặt hàng nhỏ nhất";
                sheet.Cells[1, 6].Value = "Giá  trị mặt hàng trung bình";

                int rowIndex = 2;
                foreach (var tk in month)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = tk.group;
                    sheet.Cells[rowIndex, 2].Value = tk.sum;
                    sheet.Cells[rowIndex, 3].Value = tk.count;
                    sheet.Cells[rowIndex, 4].Value = tk.max;
                    sheet.Cells[rowIndex, 5].Value = tk.min;
                    sheet.Cells[rowIndex, 6].Value = tk.avg;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }



        public ActionResult Export_Doanhthu_Loai()
        {
            //List<Report> r = new List<Report>();
            //List<Report> r = new List<Report>();
            DataContext db = new DataContext();
            var loai = db.OrderDetails.GroupBy(o => o.Product.Category)
                        .Select(o => new
                        {

                            group = o.Key.NameVN,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity),

                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });

            var stream = new MemoryStream();
            var fileName = $"Doanhthu_Loai_{DateTime.Now.ToString()}.xlsx";  // .xlsx mới đúng nha coi chừng sai xlxs


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
                sheet.Cells[1, 1].Value = "Loại hàng";
                sheet.Cells[1, 2].Value = "Tổng giá trị";
                sheet.Cells[1, 3].Value = "Số lượng";
                sheet.Cells[1, 4].Value = " Giá trị mặt hàng lớn nhất";
                sheet.Cells[1, 5].Value = " Giá trị mặt hàng nhỏ nhất";
                sheet.Cells[1, 6].Value = "Giá  trị mặt hàng trung bình";

                int rowIndex = 2;
                foreach (var tk in loai)
                {
                    // đổ dữ liệu vô từng dòng
                    sheet.Cells[rowIndex, 1].Value = tk.group;
                    sheet.Cells[rowIndex, 2].Value = tk.sum;
                    sheet.Cells[rowIndex, 3].Value = tk.count;
                    sheet.Cells[rowIndex, 4].Value = tk.max;
                    sheet.Cells[rowIndex, 5].Value = tk.min;
                    sheet.Cells[rowIndex, 6].Value = tk.avg;
                    rowIndex++;

                }
                // lưu dữ liệu
                package.Save();

            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet]
        public ActionResult TK_Tonkho()
        {

            Report rdao = new Report();

            var tonkho = rdao.Tonkho();

            return Json(new
            {
                data = tonkho
            }, JsonRequestBehavior.AllowGet);


        }




        public ActionResult TK_Doanhthu_KH()
        {
            Report rdao = new Report();

            var kh = rdao.TK_Doanhthu_KH();

            return Json(new
            {
                data = kh
            }, JsonRequestBehavior.AllowGet);



        }


        public ActionResult TK_Doanhthu_Nam()
        {
            Report rdao = new Report();

            var year = rdao.TK_Doanhthu_Nam();

            return Json(new
            {
                data = year
            }, JsonRequestBehavior.AllowGet);






        }

        public ActionResult TK_Doanhthu_Thang()
        {


            Report rdao = new Report();

            var month = rdao.TK_Doanhthu_Nam();

            return Json(new
            {
                data = month
            }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult TK_Doanhthu_Loai()
        {


            Report rdao = new Report();

            var loai = rdao.TK_Doanhthu_Loai();

            return Json(new
            {
                data = loai
            }, JsonRequestBehavior.AllowGet);


        }





        // bảng thống kê riêng kèm biểu đồ



        public ActionResult TK_Tonkho2()
        {

            return View();


        }



        public ActionResult TK_Doanhthu_KH2()
        {

            return View();


        }
        public ActionResult TK_Doanhthu_Nam2()
        {
            return View();


        }
        public ActionResult TK_Doanhthu_Thang2()
        {
            return View();
        }


        public ActionResult TK_Doanhthu_Loai2()
        {
            return View();
        }





    }

    
}