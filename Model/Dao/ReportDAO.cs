using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.InterfaceRepository;
using Model.IRepository;
using Model.EL;

namespace Model.Dao
{
     public class ReportDAO :Model.InterfaceRepository.IReport
    {
        DataContext db = new DataContext();
       /* public List<Report> Tonko()
        {
            *//*var tonkho = db.Products.GroupBy(p => p.Category).OrderByDescending(p => p.Sum(c => c.UnitPrice * c.Quantity))
               .Select(p => new Report(

                    p.Key.NameVN,
                    p.Sum(c => c.UnitPrice * c.Quantity),
                    p.Sum(c => c.Quantity),
                    p.Min(c => c.UnitPrice),
                    p.Max(c => c.UnitPrice),
                    p.Average(c => c.UnitPrice)
               ));*//*
            // var tonko = from p in Product select new Report()

            *//*var tonko2 = tonkho.ToList();*//*
            //var tonko = db.repo();
            return tonko;
        }*/
    }
}
