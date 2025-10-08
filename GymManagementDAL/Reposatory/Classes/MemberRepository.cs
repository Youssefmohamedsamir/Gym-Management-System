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
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _context;

        public MemberRepository(GymDbContext dbContext)
        {
            _context = dbContext;
        }
        public int AddMember(Member member)
        {
            _context.Members.Add(member);
            return _context.SaveChanges();
        }

        public int DeleteMember(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return 0;
            _context.Members.Remove(member);
            return _context.SaveChanges();
        }
   
        public Member GetMemberById(int id) => _context.Members.Find(id);

        public IEnumerable<Member> GetMembers() => _context.Members.ToList();

        public int UpdateMember(Member member)
        {
            _context.Members.Update(member);
            return _context.SaveChanges();
        }
    }
}
