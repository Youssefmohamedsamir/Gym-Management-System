using GymManagementBLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionservice)
        {
            _sessionService = sessionservice;
        }
        #region Get All Session
        public ActionResult Index()
        {
            var session = _sessionService.GetAllSession();
            return View(session);
        }
        #endregion

        #region Session Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var session = _sessionService.GetSessionById(id);
                if (session == null)
                {
                    TempData["ErrorMessage"] = "Session Not Found";
                    return RedirectToAction(nameof(Index));
                }
                return View(session);
            }
        }

        #endregion

        #region Create Session
        public ActionResult Create()
        {
            var Categories = _sessionService.GetCategorysForDropDown();
            ViewBag.Categories = new SelectList(Categories ,"Id","Name");

            var Trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers,"Id","Name");
            return View();
        }

        [HttpPost]

        public ActionResult Create(CreateSessionViewModel createdSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDowns();
                return View(createdSession);
            }

            var IsCreated = _sessionService.CreateSession(createdSession);
            if (IsCreated)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Session";
                LoadDropDowns();
                return View(createdSession);
            }
            
        }


        #endregion

        #region Edit Session
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionForUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainerDropDowns();
            return View(session);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id ,UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainerDropDowns();
                return View(updatedSession);
            }
            var IsUpdated = _sessionService.UpdateSession(updatedSession , id);
            if (IsUpdated)
            {
                TempData["SuccessMessage"] = "Session Updated Successfully";
                
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Session";
               
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete Session

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);
            if(Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = Session.Id;
            return View();

        }

        [HttpPost]

        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.RemoveSession(id);
            if (result)
            {
                TempData["Success Deleted"] = "Session Deleted";
            }
            else
            {

                TempData["ErrorMessage"] = "Session Can not Be Deleted";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper

        private void LoadCategoryDropDowns()
        {
            var Categories = _sessionService.GetCategorysForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");

        }
        private void LoadTrainerDropDowns()
        {
            var Trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        private void LoadDropDowns()
        {
            var Categories = _sessionService.GetCategorysForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");

            var Trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        #endregion
    }

}
