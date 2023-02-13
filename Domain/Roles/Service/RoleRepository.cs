
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Roles.Services;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    private DatabaseContext _context;
    public RoleRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }


}