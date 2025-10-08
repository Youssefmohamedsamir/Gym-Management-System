using GymManagementDAL.Data.Context;
using GymManagementDAL.Entity;
using GymManagementDAL.Reposatory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Classes
{
    internal class HealthRecordRepository : IHealthRepository
    {
        public readonly GymDbContext _context;
        public HealthRecordRepository(GymDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public int Add(HealthRecord healthRecord)
        {
            _context.HealthRecords.Add(healthRecord);
            return _context.SaveChanges();
        }

        public int Delete(HealthRecord healthRecord)
        {
            _context.HealthRecords.Remove(healthRecord);
            return _context.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAll() => _context.HealthRecords.ToList();


        public HealthRecord GetById(int id) => _context.HealthRecords.Find(id);
        

        public int Update(HealthRecord healthRecord)
        {
            _context.HealthRecords.Update(healthRecord);
            return _context.SaveChanges();
        }
    }
}
