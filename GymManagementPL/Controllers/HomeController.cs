using GymManagementBLL.Services.Interface;
using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalaticsService _analaticsService;
        public HomeController(IAnalaticsService analaticsService)
        {
            _analaticsService = analaticsService;
        }
        public ActionResult Index()
        {
            var Data = _analaticsService.GetAnalyticsData();
            
            
            return View(Data);
        }
    }
}
