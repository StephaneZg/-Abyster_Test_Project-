
using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Abyster_Test_Project.Domain.Users.Services;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private DatabaseContext _context;
    public UserRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public override IQueryable<User> FindAll(bool trackChange){
        var users =  _context.Users.Include(user => user.roles);
        return users;
    }

    public override IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression, bool trackChanges){
        var users =  _context.Users.Where(expression)
                                .Include(user => user.roles);
        return users;
    }


}