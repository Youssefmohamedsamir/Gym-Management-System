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
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext dbContext)
        {
           _context = dbContext;
        }
        public int Add(Session session)
        {
            _context.Add(session);
            return _context.SaveChanges();
        }

        public int Delete(Session session)
        {
            _context.Remove(session);
            return _context.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => _context.Sessions.ToList();
       

        public Session GetById(int id) => _context.Sessions.Find(id);
        

        public int Update(Session session)
        {
            _context.Update(session);
            return _context.SaveChanges();
        }
    }
}
