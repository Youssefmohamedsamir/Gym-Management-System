using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        [NonAction]
        public ActionResult Index()
        {
            return View();
        }
    }
}
