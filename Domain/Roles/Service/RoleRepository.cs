
using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.Domain.Roles.Services;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    private DatabaseContext _context;
    public RoleRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public override IQueryable<Role> FindAll(bool trackChange){
        var roles =  _context.Roles.Include(role => role.users);
        return roles;
    }

    public override IQueryable<Role> FindByCondition(Expression<Func<Role, bool>> expression, bool trackChanges){
        var roles =  _context.Roles.Where(expression)
                                .Include(role => role.users);
        return roles;
    }


}