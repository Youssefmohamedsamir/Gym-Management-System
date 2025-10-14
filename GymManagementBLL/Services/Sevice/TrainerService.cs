using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.PlanViewModels;
using GymManagementBLL.ViewModel.TrainerViewModel;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    internal class TrainerService : ITrainerService
    {

        private readonly IUnitOfWork _unitOfWork;
        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public bool CreateMember(CreateTrainerViewModel createTrainer)
        {
            try
            {
                //Check Phone Is Exist 

                //If One Of Them Exists Return False
                if (IsEmailExists(createTrainer.Email) && IsPhoneExists(createTrainer.Phone)) return false;
                //If Not Add Member And Return True if Added
                var Trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    Gender = createTrainer.Gender,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNo = createTrainer.BuildingNumber,
                        City = createTrainer.City,
                        Street = createTrainer.Street,
                    },
                    

                };
                _unitOfWork.GenericRepository<Trainer>().Add(Trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAll()
        {
            var Trainers = _unitOfWork.GenericRepository<Trainer>().GetAll();
            if (!Trainers.Any() || Trainers is null) return [];

            return Trainers.Select(static P => new TrainerViewModel()
            {
              Name = P.Name,
              Email = P.Email,
              Phone = P.Phone,
              Specialization = P.Specialization

            });
        }

        public TrainerViewModel GetTrainerToId(int TrainerId)
        {
            var Trainer = _unitOfWork.GenericRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null)
            {
                return null;
            }
            return new TrainerViewModel()
            {
                Name = Trainer.Name,
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                DateOfBirth = Trainer.DateOfBirth,
                Address = Trainer.Address.ToString()

            };
        
        }

        public bool RemoveStatus(int TrainerId)
        {
            var trainer = _unitOfWork.GenericRepository<Trainer>().GetById(TrainerId);
            var session = _unitOfWork.GenericRepository<Session>().GetById(TrainerId);
            if (trainer == null && session.StartDate > DateTime.Now) return false;
            {
                
                trainer.UpdatedAt = DateTime.Now;

                try
                {
                    _unitOfWork.GenericRepository<Trainer>().Update(trainer);
                    return _unitOfWork.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdatedTrainer(int TrainerId, UpdateTrainerView updated)
        {
            try
            {
                var trainer = _unitOfWork.GenericRepository<Trainer>().GetById(TrainerId);
                if (trainer is null) return false;


                //Description - price - DurationDays

                (trainer.Email, trainer.Phone, trainer.Address.BuildingNo, trainer.Address.Street , trainer.Address.City , trainer.Specialization) =
                    (updated.EmailAddress, updated.PhoneNumber, updated.BuildingNumber, updated.Street, updated.City, updated.Specialization);
                _unitOfWork.GenericRepository<Trainer>().Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception) { return false; }
        }

        public UpdateTrainerView? UpdateTrainerView(int TrainerId)
        {
            var trainer = _unitOfWork.GenericRepository<Trainer>().GetById(TrainerId);
            if ( trainer is null) return null;
            return new UpdateTrainerView()
            {
                EmailAddress = trainer.Email,
                PhoneNumber = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNo,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                Specialization = trainer.Specialization

            };
        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GenericRepository<Trainer>().GetAll(X => X.Email == email).Any();

        }
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GenericRepository<Trainer>().GetAll(X => X.Phone == phone).Any();

        }


        #endregion
    }
}
