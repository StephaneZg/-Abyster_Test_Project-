
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Users.Services;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private DatabaseContext _context;
    public UserRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}