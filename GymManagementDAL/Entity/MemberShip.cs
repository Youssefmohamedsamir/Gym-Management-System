﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public class MemberShip:BaseEntity
    {
        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                if (DateTime.Now <= EndDate)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
        }
        #region Member - MemberShip ManyToMany
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        #endregion

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
    }
}
