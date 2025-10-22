using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.AnalyticsViewModel;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    public class AnalyticsService : IAnalaticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnalyticsService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                TotalMembers = _unitOfWork.GenericRepository<MemberShip>().GetAll().Count(),
                ActiveMembers = _unitOfWork.GenericRepository<MemberShip>().GetAll(m => m.Status == "Active").Count(),
                TotalTrainers = _unitOfWork.GenericRepository<MemberShip>().GetAll().Count(),
                UpcommingSession = Sessions.Count(X => X.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(X => X.StartDate <= DateTime.Now && X.EndDate >= DateTime.Now),
                CompeletedSessions = Sessions.Count(X => X.EndDate < DateTime.Now)
            };
        }
    }
}
