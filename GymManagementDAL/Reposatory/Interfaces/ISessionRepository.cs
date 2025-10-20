using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    public interface ISessionRepository:IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();

        int GetCountOfBookesSlots(int sessionId);

        Session? GetSessionWithTrainerAndCategory(int sessionId);



    }
}
