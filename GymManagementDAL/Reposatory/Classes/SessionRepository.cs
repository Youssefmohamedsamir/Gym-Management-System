using GymManagementDAL.Data.Context;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext dbcontext;
        public SessionRepository(GymDbContext context) : base(context)
        {
         dbcontext = context;
        }
        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return dbcontext.Sessions.Include(s => s.Trainer)
                .Include(s => s.Category)
                .AsNoTracking()
                .ToList();
        }

        public int GetCountOfBookesSlots(int sessionId)
        {
            return dbcontext.MemberSessions.Count(X => X.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return dbcontext.Sessions
                .Include(s => s.Trainer)
                .Include(s => s.Category)
                .FirstOrDefault(s => s.id == sessionId);
        }
    }
}
