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

    }
}