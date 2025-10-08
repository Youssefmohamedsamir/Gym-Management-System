using GymManagementDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Reposatory.Interfaces
{
    internal interface IMemberRepository
    {
        //Get All
        IEnumerable<Member> GetMembers();
        //Get by Id
        Member GetMemberById(int id);
        //Add
        int AddMember(Member member);
        //Update
        int UpdateMember(Member member);
        //Delete
        int DeleteMember(int id);









    }
}
