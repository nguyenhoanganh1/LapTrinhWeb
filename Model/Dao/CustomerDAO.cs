using Model.EL;
using Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CustomerDAO : ICustomer
    {
        DataContext db = new DataContext();

        public List<Customer> Customers()
        {
            return db.Customers.ToList();
        }

        public void DeleteCustomer(Customer customer)
        {
            db.Customers.Remove(customer);
            this.Save();
        }

        public Customer GetCustomerById(string Id)
        {
            return db.Customers.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void InsertCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            this.Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            var cus = db.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
            if (cus != null)
            {
                cus.Password = customer.Password;
                cus.Fullname = customer.Fullname;
                cus.Email = customer.Email;
                cus.Photo = customer.Photo;
                cus.Activated = customer.Activated;
                cus.Admin = customer.Admin;
                this.Save();
            }
        }
    }
}
