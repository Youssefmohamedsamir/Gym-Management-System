using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.PlanViewModels
{
    internal class UpdatePlanViewModel
    {
        [Required(ErrorMessage ="PlanName Is Requird")]
        [StringLength(50, ErrorMessage = "Plan Name Must Be Less Than 51 Char")]
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "Description Is Requird")]
        [StringLength(50, MinimumLength = 5 , ErrorMessage = "Description Name Must Between 5 And 200")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "DurationDays Is Requird")]
        [Range(1 , 365 , ErrorMessage = "Must be Between 1 and 365")]
        public int DurationDays { get; set; }

        [Range(0.1, 10000, ErrorMessage = "Must be Between 0.1 and 10000")]

        public decimal Price { get; set; }
       
    }

}
