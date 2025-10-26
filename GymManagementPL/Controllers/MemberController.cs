using AspNetCoreGeneratedDocument;
using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.MemberViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        #region GetAllMembers
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get MemberData
        //BaseUrl/Member/MemberDetails/id
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            
                
            var Member = _memberService.GetMemberDetails(id);
            if (Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
                

            return View(Member);
        }


        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
                
            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (HealthRecord is null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found";
                return RedirectToAction(nameof(Index));
            }
                
            return View (HealthRecord);
        }



        #endregion

        #region Create Member
        public ActionResult Create()
        {
            return View("CreateMember");
        }
        [HttpPost]
        public ActionResult CreateMember(CreateViewModel createdMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError( "DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), createdMember);
            }
            var IsCreated = _memberService.CreateMember(createdMember);
            if (!IsCreated)
            {
                TempData["ErrorMessage"] = "Email Or Phone Already Exists";
                return RedirectToAction(nameof(Create));
            }
            TempData["SuccessMessage"] = "Member Created Successfully";
            return  RedirectToAction(nameof(Index));


        }
        #endregion

        #region Edit Member

        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
                
            var Member = _memberService.GetMemberToUpdate(id);
            if (Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
                
            return View("MemberEdit" , Member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id ,MemberToUpdateViewModel Membertoupdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View( Membertoupdate);
            }
            var IsUpdated = _memberService.UpdateMember(id , Membertoupdate);
            if (!IsUpdated)
            {
                TempData["ErrorMessage"] = "Email Or Phone Already Exists";
               
            }
            TempData["SuccessMessage"] = "Member Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete Member

        public ActionResult Delete (int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberDetails(id);
            if (Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed ([FromForm]int id)
        {
            var IsDeleted = _memberService.RemoveMember(id);
            if (!IsDeleted)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Member Can Not Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }



        #endregion
    }
}
