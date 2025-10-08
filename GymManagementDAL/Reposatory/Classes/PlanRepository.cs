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
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _context;

        public PlanRepository(GymDbContext dbContext)
        {
        _context = dbContext;
        }
        public int Add(Plan plan)
        {
            _context.Plans.Add(plan);
           return _context.SaveChanges();
        }

        public int Delete(Plan plan)
        {
            _context.Plans.Remove(plan);
            return _context.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => _context.Plans.ToList();
        

        public Plan GetById(int id) => _context.Plans.Find(id);
        

        public int Update(Plan plan)
        {
            _context.Plans.Update(plan);
            return _context.SaveChanges();
        }
    }
}
