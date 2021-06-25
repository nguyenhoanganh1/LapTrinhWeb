using Lib.Data;
using Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetAllCategories();
    }
    public class CategoryRepository
    {
        public CategoryRepository(DbContextFactory factory)
            : base(factory)
        {

        }
        public List<Category> GetAllRooms()
        {
            var query = dataContext.Room.AsQueryable();
            return query.ToList();
        }
    }
}
