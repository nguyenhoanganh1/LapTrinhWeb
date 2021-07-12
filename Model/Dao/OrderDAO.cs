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
    }
}
