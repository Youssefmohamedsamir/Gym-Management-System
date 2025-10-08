using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entity
{
    public class HealthRecord : BaseEntity
    {
        public int Height { get; set; } 

        public int Weight { get; set; }

        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }


        //LastUpdate => UpdatedAt in BaseEntity
    }
}
