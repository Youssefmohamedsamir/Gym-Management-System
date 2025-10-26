﻿using GymManagementBLL.ViewModels.TrainerViewModels;

namespace GymManagementSystemBLL.Services.Interfaces
{
	public interface ITrainerService
	{
		IEnumerable<TrainerViewModel> GetAllTrainers();
		bool CreateTrainer(CreateTrainerViewModel createdTrainer);
		TrainerViewModel? GetTrainerDetails(int trainerId);
		TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId, TrainerToUpdateViewModel trainerToUpdateViewModel);
		bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId);
		bool RemoveTrainer(int trainerId);
	}
}
