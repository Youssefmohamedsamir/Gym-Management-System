using GymManagementDAL.Data.Context;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Classes
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _context;

        public CategoryRepository(GymDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public int Add(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges();
        }

        public int Delete(Category category)
        {
            _context.Categories.Remove(category);
            return _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();


        public Category GetById(int id) => _context.Categories.Find(id);
       

        public int Update(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChanges();
        }
    }
}

