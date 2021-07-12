using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDAO
    {
        private DataContext db = new DataContext();

        public List<Order> GetAllOrders()
        {
            return db.Orders.ToList();
        }

        public List<Order> FindAllOrderById(string id)
        {

            return db.Orders.Where(x => x.CustomerId == id).OrderBy(x => x.Id).ToList();
        }

        public Order GetOrderById(long Id)
        {
            return db.Orders.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateStatus(Order o)
        {
            var oder = db.Orders.FirstOrDefault(x => x.Id == o.Id);
            if (oder != null)
            {
                oder.Address = o.Address;
                oder.Amount = o.Amount;
                oder.CustomerId = o.CustomerId;
                oder.Notes = o.Notes;
                oder.OrderDate = o.OrderDate;
                oder.Status = o.Status;
                this.Save();
            }
        }
      
    }
}
