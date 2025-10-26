using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.MemberViewModel
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage ="Hight is Requird")]
        [Range(0.1 , 300 , ErrorMessage ="Height Must Be Greater Than 0 And Less Than 300")]

        public decimal Hight { get; set; }

        [Required(ErrorMessage = "Weight is Requird")]
        [Range(0.1, 300, ErrorMessage = "weight Must Be Greater Than 0 And Less Than 300")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "BloodType is Requird")]
        [StringLength(3, ErrorMessage = "Blood Type Must Be 3 Char Or Less")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }
    }
}
