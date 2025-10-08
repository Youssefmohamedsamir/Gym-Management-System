using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    internal interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category GetById(int id);

        int Add(Category category);
        int Update(Category category);
        int Delete(Category category);

    }
}
