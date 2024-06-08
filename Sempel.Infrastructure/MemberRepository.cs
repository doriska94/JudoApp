using Stempel.Domain.Model;
using Stempel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sempel.Infrastructure;
public class MemberRepository : IMemberRepository
{
    private List<Member> _members = new()
    {
        new Member()
        {
            Id = 1,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            State = State.Levead,
            FirstName = "Nikole",
            IsDeleted = false,
            Key ="1912896549"
        }
    };
    public Member Get(string code)
    {
        return _members.Where(x => x.Key == code).Single();

    }
}
