using GymManagementDAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.TrainerViewModel
{
    public class UpdateTrainerView
    {
        public string? Name { get; set; }

        public string? EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int BuildingNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public Specialities Specialization { get; set; }

    }
}
