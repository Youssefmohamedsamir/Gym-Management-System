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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _context;

        public TrainerRepository(GymDbContext dbContext)
        {
         _context = dbContext;
        }

        public int Add(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            return _context.SaveChanges();
        }

        public int Delete(Trainer trainer)
        {
            _context.Trainers.Remove(trainer);
            return _context.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => _context.Trainers.ToList();
        

        public Trainer GetbyId(int id) =>_context.Trainers.Find(id);
        

        public int Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            return _context.SaveChanges();
        }
    }
}
