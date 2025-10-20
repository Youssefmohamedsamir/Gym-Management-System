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
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity, new();
        object GetRepository<T>();
        int SaveChanges();
    }
}
