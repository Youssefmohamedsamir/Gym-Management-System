using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    internal interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAll();

        Trainer GetbyId(int id);

        int Add(Trainer trainer);

        int Update(Trainer trainer);

        int Delete(Trainer trainer);
    }
}
