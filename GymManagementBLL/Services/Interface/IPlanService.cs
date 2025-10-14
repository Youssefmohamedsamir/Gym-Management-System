using GymManagementBLL.ViewModel.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interface
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel GetPlanById(int planId);

        UpdatePlanViewModel? GetPlanToUpdate(int planId);

        bool UpdatePlan (int PlanId, UpdatePlanViewModel updatedplan);

       bool ToggelStatus(int planId);
        

        
    }
}
