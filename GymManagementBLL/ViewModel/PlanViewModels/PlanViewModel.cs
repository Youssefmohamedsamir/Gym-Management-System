using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.PlanViewModels
{
    internal class PlanViewModel
    {
        public int planId { get; set; }

        public string? Name { get; set; } = null;

        public string? Description { get; set; } = null;

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        public bool IsActive {get; set; }
        //public string Decription { get;  set; } = null!;
    }
}
