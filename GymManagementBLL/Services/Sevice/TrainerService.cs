using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using GymManagementSystemBLL.Services.Interfaces;

namespace GymManagementSystemBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            try
            {
                var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();

                if (IsEmailExists(createdTrainer.Email) || IsPhoneExists(createdTrainer.Phone))
                    return false;

                var trainer = new Trainer()
                {
                    Name = createdTrainer.Name,
                    Email = createdTrainer.Email,
                    Phone = createdTrainer.Phone,
                    DateOfBirth = createdTrainer.DateOfBirth,
                    Specialties = createdTrainer.Specialties,
                    Gender = createdTrainer.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = createdTrainer.BuildingNumber,
                        City = createdTrainer.City,
                        Street = createdTrainer.Street,
                    }
                };

                repo.Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();
            var trainers = repo.GetAll();

            if (trainers is null || !trainers.Any())
                return new List<TrainerViewModel>();

            return trainers.Select(x => new TrainerViewModel
            {
                Id = x.id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialties = x.Specialties.ToString()
            });
        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();
            var trainer = repo.GetById(trainerId);
            if (trainer is null) return null;

            return new TrainerViewModel
            {
                Email = trainer.Email,
                Name = trainer.Name,
                Phone = trainer.Phone,
                Specialties = trainer.Specialties.ToString()
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId, TrainerToUpdateViewModel trainerToUpdateViewModel)
        {
            var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();
            var trainer = repo.GetById(trainerId);
            if (trainer is null) return null;

            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Street = trainer.Address.Street,
                BuildingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Specialties = trainer.Specialties
            };
        }

        public bool RemoveTrainer(int trainerId)
        {
            var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();
            var trainerToRemove = repo.GetById(trainerId);
            if (trainerToRemove is null || HasActiveSessions(trainerId))
                return false;

            repo.Delete(trainerToRemove);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId)
        {
            var repo = (IGenericRepository<Trainer>)_unitOfWork.GenericRepository<Trainer>();
            var trainerToUpdate = repo.GetById(trainerId);

            if (trainerToUpdate is null || IsEmailExists(updatedTrainer.Email) || IsPhoneExists(updatedTrainer.Phone))
                return false;

            trainerToUpdate.Email = updatedTrainer.Email;
            trainerToUpdate.Phone = updatedTrainer.Phone;
            trainerToUpdate.Address.BuildingNumber = updatedTrainer.BuildingNumber;
            trainerToUpdate.Address.Street = updatedTrainer.Street;
            trainerToUpdate.Address.City = updatedTrainer.City;
            trainerToUpdate.Specialties = updatedTrainer.Specialties;
            trainerToUpdate.UpdatedAt = DateTime.Now;

            repo.Update(trainerToUpdate);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helper Methods

        private bool IsEmailExists(string email)
        {
            var repo = (IGenericRepository<Member>)_unitOfWork.GenericRepository<Member>();
            return repo.GetAll(m => m.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            var repo = (IGenericRepository<Member>)_unitOfWork.GenericRepository<Member>();
            return repo.GetAll(m => m.Phone == phone).Any();
        }

        private bool HasActiveSessions(int id)
        {
            var repo = (IGenericRepository<Session>)_unitOfWork.GenericRepository<Session>();
            return repo.GetAll(s => s.TrainerId == id && s.StartDate > DateTime.Now).Any();
        }

        #endregion
    }
}