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

        public List<Product> FindByKeywords(string Keywords)
        {

            var list = db.Products.Where(x => x.Name.Contains(Keywords)
                                          || x.Category.Name.Contains(Keywords)
                                          || x.Category.NameVN.Contains(Keywords)).ToList();
            return list;
        } // ok
        public List<Product> FindByCategory(int id)
        {
            var list2 = db.Products.Where(c => c.CategoryId == id).ToList();
            return list2;
        }
        public List<Product> FindBySpecials()
        {
            var special = db.Products.Where(x => x.Special == true).ToList();

            return special;
        }
        public List<Product> FindByMostView()
        {
            var mostview = db.Products.Where(x => x.ClickCount > 0).OrderByDescending(x => x.ClickCount).ToList();
            return mostview;

        }
        public List<Product> FindBySaleOff()
        {
            var sale = db.Products.Where(x => x.Discount > 0).OrderByDescending(x => x.Discount).ToList();
            return sale;
        }
        public List<Product> FindByLatest()
        {
            var latest = db.Products.Where(x => x.Latest == true).ToList();
            return latest;
        }
       public  List<Product> FindByBestSeller()
        {
            var best = db.Products.ToList();
            return best;
        }
        public List<Product> FindAll()
        {
            var all = db.Products.ToList();
            return all;
        }
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
