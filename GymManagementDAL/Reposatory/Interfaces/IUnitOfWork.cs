using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChanges();
    }
}
