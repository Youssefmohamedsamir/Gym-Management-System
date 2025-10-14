using GymManagementDAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.TrainerViewModel
{
    internal class TrainerViewModel
    {
       
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 And 50 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid email Format")]
        [DataType(DataType.EmailAddress)] // UI HINT
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 And 100 Char")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Requird")]
        [DataType(DataType.PhoneNumber)] // UI HINT
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyption Number")]
        public string? Phone { get; set; } 
        [Required(ErrorMessage = "Specialization Is Requird")]

        public Specialities Specialization { get; set; }
        [Required(ErrorMessage = "DateOfBirth Is Requird")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Address Is Requird")]
        public string? Address { get; set; }

    }
}
