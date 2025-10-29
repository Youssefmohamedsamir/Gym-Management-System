using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public class Member : GymUser
    {
        //JoinDate => CreatedAt in BaseEntity
        public string Photo { get; set; } = null!;

        #region HealthRecord - Member
        public int HealthRecordId { get; set; }
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - MemberShip ManyToMany
        public ICollection<MemberShip> MemberShips { get; set; } = null!;

        #endregion

        #region Member - MemberSession Many To Many
        public ICollection<MemberSession> MemberSessionsInMember { get; set; } = null!;

        #endregion


    }
}
