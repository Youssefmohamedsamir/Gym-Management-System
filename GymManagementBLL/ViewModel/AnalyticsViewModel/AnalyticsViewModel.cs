using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModel.AnalyticsViewModel
{
    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int UpcommingSession { get; set; }
        public int OngoingSessions { get; set; }
        public int CompeletedSessions { get; set; }
    }
}
