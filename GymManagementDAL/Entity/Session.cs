using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public class Session : BaseEntity
    {
        #region Properties
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region RelationShip[Session - Category]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        #endregion

        #region RelationShip [Session - Trainer]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;
        #endregion

        #region Relation [Session - MemberSession]
        public ICollection<MemberSession> MemberSessionsInSession { get; set; } = null!;
        #endregion
    }
}
