using Model.EL;
using Model.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDAO : ICategory
    {
        DataContext db = new DataContext();

        public List<Category> categories()
        {
            return db.Categories.ToList();
        }

        public void DeleteCategory(Category category)
        {
            db.Categories.Remove(category);
            this.Save();
        }

        public Category GetCategoryById(int Id)
        {
            return db.Categories.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void InsertCategory(Category category)
        {
            db.Categories.Add(category);
            this.Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        // phương thức này chỉ cần truyền vào 1 oject thôi là xong
        public void UpdateCategory(Category category)
        {
            var cate = db.Categories.Where(x => x.Id == category.Id).FirstOrDefault();
            if (cate != null)
            {
                cate.Name = category.Name;
                cate.NameVN = category.NameVN;
                this.Save();
            }
        }
    }
}
