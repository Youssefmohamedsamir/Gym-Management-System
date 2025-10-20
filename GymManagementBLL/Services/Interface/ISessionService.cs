using GymManagementBLL.ViewModel.SessionViewModel;
using GymManagementDAL.Entity;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interface
{
    internal interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();

        SessionViewModel GetSessionById(int sessionId);

        bool CreateSession(SessionViewModel CreatedSession);

       UpdateSessionViewModel? GetSessionForUpdate(int sessionId);
        bool UpdateSession(UpdateSessionViewModel UpdatedSession , int sessionId);

        bool RemoveSession(int sessionId);






    }
}
