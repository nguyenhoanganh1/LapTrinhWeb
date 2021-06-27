using Model.EL;
using Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDAO : IProduct
    {
        DataContext db = new DataContext();

        public void DeleteProduct(Product product)
        {
            db.Products.Remove(product);
            this.Save();
        }

        public Product GetProductById(int Id)
        {
            return db.Products.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void InsertProduct(Product product)
        {
            db.Products.Add(product);
            this.Save();
        }

        public List<Product> Products()
        {
            return db.Products.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var pro = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (pro != null)
            {
                pro.Name = product.Name;
                pro.Image = product.Image;
                pro.UnitPrice = product.UnitPrice;
                pro.Discount = product.Discount;
                pro.ProductDate = product.ProductDate;
                pro.Special = product.Special;
                pro.ClickCount = product.ClickCount;
                pro.Description = product.Description;
                this.Save();
            }
        }
    }
}
