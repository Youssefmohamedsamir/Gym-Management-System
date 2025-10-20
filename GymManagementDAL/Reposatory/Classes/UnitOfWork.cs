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
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Dictionary<Type, object> _repositoryeDictionary = new();
        private readonly GymDbContext context;

        public UnitOfWork(GymDbContext context , ISessionRepository sessionRepository)
        {
            this.context = context;
            SessionRepository = sessionRepository;
        }

        public ISessionRepository SessionRepository { get; }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity, new()
        {
          var EntityType = typeof(TEntity);
            if (_repositoryeDictionary.TryGetValue(EntityType, out var Repo))
                return (IGenericRepository<TEntity>) Repo;

            var newRepo = new GenericRepository<TEntity>(context);
            _repositoryeDictionary[EntityType] = newRepo;
            return newRepo;
        }

        public object GetRepository<T>()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
