using GymManagementDAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.MemberViewModel
{
    internal class CreateViewModel
    {

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 And 50 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]

        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid email Format")]//Validation
        [DataType(DataType.EmailAddress)] // UI HINT
        [StringLength(100 , MinimumLength = 5 , ErrorMessage ="Email Must Be Between 5 And 100 Char")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Requird")]//Validation
        [DataType(DataType.PhoneNumber)] // UI HINT
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyption Number")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Gender Is Requird")]//Validation
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "BuildingNumber Is Requird")]
        [Range(1 , 9000 , ErrorMessage = "BuildingNumber Must Be Between 1 And 9000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street Is Requird")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 And 30 Char")]
        public string Street { get; set; } = null !;
        [Required(ErrorMessage = "City Is Requird")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 And 30 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "DateOfBirth Is Requird")]
        public DateOnly DateOfBirth { get; set; }

        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;
    }
}
