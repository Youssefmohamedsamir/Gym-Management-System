using AutoMapper;
using GymManagementBLL.ViewModel.SessionViewModel;
using GymManagementDAL.Entity;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());
            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>();

            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();


        }
    }
}
