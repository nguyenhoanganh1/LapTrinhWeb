using Lib.Data;
using Lib.Entities;
using Lib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Services
{
    public class CategoryService
    {
        private IUnitOfWork unitOfWork;
        private CategoryRepository categoryRepository;
        public CategoryService()
        {
            var dbContextFactory = new DbContextFactory();
            unitOfWork = new UnitOfWork(dbContextFactory);
            categoryRepository = new CategoryRepository(dbContextFactory);
        }
        public void Save()
        {
            unitOfWork.Commit();
        }
        public void insertRoom(Category r)
        {
            categoryRepository.Add(r);
            Save();
        }
        public List<Category> getAllRoom()
        {
            return categoryRepository.GetAllRooms();
        }
    }
}
