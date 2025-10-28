using GymManagementBLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planservice)
        {
            _planService = planservice;
        }
        #region Get All Plans
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        #endregion

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var plan = _planService.GetPlanById(id);
                if (plan == null)
                {
                    TempData["ErrorMessage"] = "Plan Not Found";
                    return RedirectToAction(nameof(Index));
                }
                return View(plan);
            }
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var planToUpdate = _planService.GetPlanToUpdate(id);
                if (planToUpdate == null)
                {
                    TempData["ErrorMessage"] = "Plan Not Found";
                    return RedirectToAction(nameof(Index));
                }
                return View(planToUpdate);
            }

        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id, GymManagementBLL.ViewModel.PlanViewModels.UpdatePlanViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(updatedPlan);
            }
            
                var isUpdated = _planService.UpdatePlan(id, updatedPlan);
                if (isUpdated)
                {
                    TempData["SuccessMessage"] = "Plan Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to Update Plan";
                    
                }
            return RedirectToAction(nameof(Index));
            
        }
        [HttpPost]
        public ActionResult Activate(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var isToggled = _planService.ToggelStatus(id);
                if (isToggled)
                {
                    TempData["SuccessMessage"] = "Plan Status Toggled Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to Toggle Plan Status";
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
