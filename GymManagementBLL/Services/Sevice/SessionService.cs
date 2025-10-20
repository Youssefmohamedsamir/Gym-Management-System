using AutoMapper;
using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.SessionViewModel;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(SessionViewModel CreatedSession)
        {
            try
            {
                if (!IsTrainerExist(CreatedSession.Id) || !IsCategoryExist(CreatedSession.Id) || !IsDateValid(CreatedSession.StartDate, CreatedSession.EndDate))
                    return false;

                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity < 0)
                    return false;

                var SessionEntity = _mapper.Map<Session>(CreatedSession);
                _unitOfWork.GenericRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failf : {ex}");
                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if ( !Sessions.Any())
                return [];

            var MappedSessions = _mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in MappedSessions)
                 session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookesSlots(session.Id);
            return MappedSessions;

        }

        public SessionViewModel GetSessionById(int sessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (Session is null) return null;
           
            var MappedSession = _mapper.Map<Session, SessionViewModel>(Session);
            MappedSession.AvailableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookesSlots(MappedSession.Id);

            return MappedSession;
        }

        public UpdateSessionViewModel? GetSessionForUpdate(int sessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetById(sessionId);
            if (!IsSessionAvailableForUpdate(Session!))
                return null;

            return _mapper.Map<UpdateSessionViewModel>(Session!);
        }

        public bool UpdateSession(UpdateSessionViewModel UpdatedSession, int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForUpdate(Session!))
                    return false;
                if (!IsTrainerExist(UpdatedSession.TrainerId) || !IsDateValid(UpdatedSession.StartDate, UpdatedSession.EndDate)) 
                    return false;

                _mapper.Map(UpdatedSession, Session);
                Session!.UpdatedAt = DateTime.Now;

                _unitOfWork.SessionRepository.Update(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Faild : {ex}");
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try
            {
             var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForRemove(Session!))
                    return false;
                _unitOfWork.SessionRepository.Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Session Faild : {ex}");
                return false;
            }
        }

        #region Helper Method

        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session is null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookesSlots(session.id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }
        private bool IsSessionAvailableForRemove(Session session)
        {
            if (session is null) return false;
            if (session.StartDate > DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookesSlots(session.id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }

        private bool IsTrainerExist(int TrainerId)
        {
            return _unitOfWork.GenericRepository<Trainer>().GetById(TrainerId) is not null;
        }

        private bool IsCategoryExist(int CategoryId)
        {
            return _unitOfWork.GenericRepository<Category>().GetById(CategoryId) is not null;
        }

        private bool IsDateValid(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }

       


        #endregion


    }
}
