using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IRepository
{
    public interface ICustomer
    {
        List<Customer> Customers();
        Customer GetCustomerById(string Id);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void Save();
    }
}
