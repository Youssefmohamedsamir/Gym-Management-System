using GymManagementBLL.ViewModel.MemberViewModel;
using GymManagementBLL.ViewModel.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interface
{
    internal interface ITrainerService
    {

        IEnumerable<TrainerViewModel> GetAll();

        bool CreateMember(CreateTrainerViewModel createTrainer);

        TrainerViewModel GetTrainerToId(int TrainerId);

        UpdateTrainerView? UpdateTrainerView(int TrainerId);

        bool UpdatedTrainer(int TrainerId, UpdateTrainerView updated);

        bool RemoveStatus(int TrainerId);

    }
}
