using GymManagementDAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public class Trainer : GymUser
    {
        public Specialities Specialization { get; set; }

        //HireDate => CreatedAt in BaseEntity
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
