using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.PlanViewModels;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GenericRepository<Plan>().GetAll();
            if (!Plans.Any() || Plans is null) return [];

            return Plans.Select(static P => new PlanViewModel()
            {
                planId = P.id,
                Description = P.Description,
                DurationDays = P.DurationDays,
                IsActive = P.IsActive,
                Price = P.Price,

            });

        }

        public PlanViewModel GetPlanById(int planId)
        {
            var plan = _unitOfWork.GenericRepository<Plan>().GetById(planId);
            if (plan is null)
            {
                return null;
            }
            return new PlanViewModel()
            {
                planId = plan.id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive,
                Price = plan.Price,
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GenericRepository<Plan>().GetById(planId);
            if (plan?.IsActive == false || plan is null || HasActiveMemberShips(planId)) return null;
            return new UpdatePlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                PlanName = plan.Name,
                Price = plan.Price

            };



        }
        
        public bool ToggelStatus(int planId)
        {
            var plan = _unitOfWork.GenericRepository<Plan>().GetById(planId);
            if (plan == null || HasActiveMemberShips(planId)) return false;
            {
                plan.IsActive = plan.IsActive = true ? false : true;
                plan.UpdatedAt = DateTime.Now;

                try
                {
                    _unitOfWork.GenericRepository<Plan>().Update(plan);
                    return _unitOfWork.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedplan)
        {
            try
            {
                var plan = _unitOfWork.GenericRepository<Plan>().GetById(PlanId);
                if (plan is null || HasActiveMemberShips(PlanId)) return false;


                //Description - price - DurationDays

                (plan.Description, plan.Price, plan.DurationDays, plan.UpdatedAt) =
                    (updatedplan.Description, updatedplan.Price, updatedplan.DurationDays, DateTime.Now);
                _unitOfWork.GenericRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception) { return false; }
        }




        #region Helper

        private bool HasActiveMemberShips(int planId)
        {
            var ActiveMemberShip = _unitOfWork.GenericRepository<MemberShip>().GetAll(X => X.PlanId == planId && X.Status == "Active");
            return ActiveMemberShip.Any();
        }



        #endregion
    }
    
}
