using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {
        string? Name { get; set; }

        TEntity? GetById(int Id);
        IEnumerable<TEntity> GetAll(Func<TEntity , bool>? condition = null );
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(TEntity entity);

    }
}
