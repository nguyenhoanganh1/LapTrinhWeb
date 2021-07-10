using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IRepository
{
    public interface IProduct
    {
        List<Product> FindByKeywords(string Keywords); // ok
        List<Product> FindByCategory(int id);//
        List<Product> FindBySpecials();//
        List<Product> FindByMostView();//
        List<Product> FindBySaleOff();//
        List<Product> FindByLatest();//
        List<Product> FindByBestSeller();//
        List<Product> FindAll();//
        List<Product> Products();
        Product GetProductById(int Id);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void Save();
    }
}
