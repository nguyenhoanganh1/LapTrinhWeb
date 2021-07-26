using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.ReportServices
{
    public class Report
    {
        DataContext db = new DataContext();
        public object Tonkho()
        {
            var tonkho = db.Products.GroupBy(p => p.Category).OrderByDescending(p => p.Sum(c => c.UnitPrice * c.Quantity))
                       .Select(p => new
                       {
                           group = p.Key.NameVN,
                           sum = p.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                           count = p.Sum(c => c.Quantity),
                           min = p.Min(c => c.UnitPrice),
                           max = p.Max(c => c.UnitPrice),
                           avg = p.Average(c => c.UnitPrice)
                       });
            return tonkho;
        }



        public object TK_Doanhthu_KH()
        {
            DataContext db = new DataContext();
            var kh = db.OrderDetails.GroupBy(o => o.Order.Customer).Select(o => new
            {
                group = o.Key.Fullname,
                sum = o.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                count = o.Sum(p => p.Quantity),
                min = o.Min(p => p.UnitPrice),
                max = o.Max(p => p.UnitPrice),
                avg = o.Average(p => p.UnitPrice)
            });
            return kh;
        }



        public object TK_Doanhthu_Loai()
        {
            DataContext db = new DataContext();
            var loai = db.OrderDetails.GroupBy(o => o.Product.Category)
                        .Select(o => new
                        {
                            group = o.Key.NameVN,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });
            return loai;
        }



        public object TK_Doanhthu_Nam()
        {
            DataContext db = new DataContext();
            var year = db.OrderDetails.GroupBy(o => o.Order.OrderDate.Year)
                        .Select(o => new
                        {
                            group = o.Key,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });
            return year;
        }

        public object TK_Doanhthu_Quy()
        {
            DataContext db = new DataContext();
            var Quarter = db.OrderDetails
                        .Where(x => x.Order.Status == 3)
                        .GroupBy(o => o.Order.OrderDate.Month / 3.0)
                        .Select(o => new
                        {
                            group = o.Key,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });
            return Quarter;
        }

        public object TK_Doanhthu_Thang()
        {
            DataContext db = new DataContext();
            var month = db.OrderDetails.GroupBy(o => o.Order.OrderDate.Month)
                        .Select(o => new
                        {
                            group = o.Key,
                            sum = o.Sum(c => c.UnitPrice * c.Quantity * (1 - c.Discount)),
                            count = o.Sum(c => c.Quantity),
                            min = o.Min(c => c.UnitPrice),
                            max = o.Max(c => c.UnitPrice),
                            avg = o.Average(c => c.UnitPrice)
                        });
            return month;
        }




    }
}