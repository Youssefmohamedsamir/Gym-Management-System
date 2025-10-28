using GymManagementBLL.Services.Interface;
using GymManagementBLL.Services.Sevice;
using GymManagementBLL.ViewModel.MemberViewModel;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entity;
using GymManagementSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        #region Get All Trainers
        public ActionResult Index()
        {
            var Trainers = _trainerService.GetAllTrainers();
            return View(Trainers);
        }
        #endregion

        #region Get Trainer Data
        //BaseUrl/Member/MemberDetails/id
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }


            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }


            return View(Trainer);
        }


       



        #endregion

        #region Create Trainer
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), createdTrainer);
            }
            var IsCreated = _trainerService.CreateTrainer(createdTrainer);
            if (!IsCreated)
            {
                TempData["ErrorMessage"] = "Email Or Phone Already Exists";
                return RedirectToAction(nameof(Create));
            }
            TempData["SuccessMessage"] = "Trainer Created Successfully";
            return RedirectToAction(nameof(Index));


        }
        #endregion

        #region Edit Trainer

        public ActionResult TrainerEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View("TrainerEdit", Trainer);
        }
        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(trainerToUpdateViewModel);
            }

            var isUpdated = _trainerService.UpdateTrainerDetails(trainerToUpdateViewModel, id);

            if (!isUpdated)
            {
                TempData["ErrorMessage"] = "Email Or Phone Already Exists";
                return View(trainerToUpdateViewModel);
            }

            TempData["SuccessMessage"] = "Trainer Updated Successfully";
            return RedirectToAction(nameof(Index));
        }

        #endregion

            #region Delete Trainer

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            ViewBag.TrainerName = Trainer.Name;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
            var IsDeleted = _trainerService.RemoveTrainer(id);
            if (!IsDeleted)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Trainer Can Not Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }



        #endregion
    }
}
