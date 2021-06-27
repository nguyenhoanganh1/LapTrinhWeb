using Model.EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IRepository
{
    public interface ICategory
    {
        List<Category> categories();
        Category GetCategoryById(int Id);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        void Save();
    }
}
