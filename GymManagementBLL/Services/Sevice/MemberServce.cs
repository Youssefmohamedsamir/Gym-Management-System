using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.MemberViewModel;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Classes;
using GymManagementDAL.Reposatory.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    internal class MemberServce : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberServce(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberViewModel> GetAll()
        {
            var data = _unitOfWork.GenericRepository<Member>().GetAll();

            if (data == null || !data.Any()) return [];

            var members = new List<MemberViewModel>();
            foreach (var item in data)
            {
             var member = new MemberViewModel()
             {
                 Id = item.id,
                 Name = item.Name,
                 Email = item.Email,
                 Phone = item.Phone,
                 Gender = item.Gender.ToString(),
                 Photo = item.Photo
             };
                members.Add(member);
            }
            return members;
        }

        public bool CreateMember(CreateViewModel createmember)
        {
            try
            {
                //Check Phone Is Exist 
      
                //If One Of Them Exists Return False
                if (IsEmailExists(createmember.Email) && IsPhoneExists(createmember.Phone)) return false;
                //If Not Add Member And Return True if Added
                var member = new Member()
                {
                    Name = createmember.Name,
                    Email = createmember.Email,
                    Phone = createmember.Phone,
                    Gender = createmember.Gender,
                    DateOfBirth = createmember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNo = createmember.BuildingNumber,
                        City = createmember.City,
                        Street = createmember.Street,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = (int)createmember.HealthRecordViewModel.Hight,
                        Weight = (int)createmember.HealthRecordViewModel.Weight,
                        BloodType = createmember.HealthRecordViewModel.BloodType,
                        Note = createmember.HealthRecordViewModel.Note,
                    }

                };
                 _unitOfWork.GenericRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = _unitOfWork.GenericRepository<Member>().GetById(MemberId);
            if (member == null) return null;

            var ViewModel = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNo} - {member.Address.Street}-{member.Address.City}",
                Photo = member.Photo,

            };

            // Active memberShip

            var Activemembership = _unitOfWork.GenericRepository<MemberShip>().GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                .FirstOrDefault();
            if (Activemembership is not null)
            {
                ViewModel.MemberShipStartDate = Activemembership.CreatedAt.ToShortDateString();
                ViewModel.MemberShipEndDate = Activemembership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GenericRepository<Plan>();
                ViewModel.PlanName = plan.Name;
            }

            return ViewModel;

        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var Repo = _unitOfWork.GenericRepository<HealthRecord>();
            var MemberHealthRecord = Repo.GetById(MemberId);
            if (MemberHealthRecord is null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Hight = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var Repo = _unitOfWork.GenericRepository<Member>();
            var Member = Repo.GetById(MemberId);
            if (Member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = Member.Email,
                Name = Member.Name,
                Phone = Member.Phone,
                Photo = Member.Photo,
                BuildingNumber = Member.Address.BuildingNo,
                City = Member.Address.City,
                Street = Member.Address.Street,
            };
        }

        public bool UpdateMember(int id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
               
                if (IsEmailExists(UpdatedMember.Email) && IsPhoneExists(UpdatedMember.Phone)) return false;

                var Repo = _unitOfWork.GenericRepository<Member>();
                var Member = Repo.GetById(id);
                if (Member is null) return false;

                Member.Email = UpdatedMember.Email;
                Member.Phone = UpdatedMember.Phone;
                Member.Address.BuildingNo = UpdatedMember.BuildingNumber;
                Member.Address.Street = UpdatedMember.Street;
                Member.Address.City = UpdatedMember.City;
                Member.UpdatedAt = DateTime.Now;

                 Repo.Update(Member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
return false;
            } 
        }

        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GenericRepository<Member>();
            var member = MemberRepo.GetById(MemberId);
            if (member is null) return false;
            var HasActiveMemberSessions = _unitOfWork.GenericRepository<MemberSession>()
                .GetAll(X => X.MemberId == MemberId && X.Session.StartDate > DateTime.Now);
            if (HasActiveMemberSessions.Any()) return false;

            var MemberShips = _unitOfWork.GenericRepository<MemberShip>().GetAll(X => X.MemberId == MemberId);

            try
            {
                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                    {
                        _unitOfWork.GenericRepository<MemberShip>().Delete(memberShip);
                    }
                }
                MemberRepo.Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GenericRepository<Member>().GetAll(X => X.Email == email).Any();

        }
        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GenericRepository<Member>().GetAll(X => X.Phone == phone).Any();

        }

       
        #endregion
    }
}
