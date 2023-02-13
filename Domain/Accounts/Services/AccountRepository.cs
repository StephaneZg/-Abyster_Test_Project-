
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Accounts.Services;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    private DatabaseContext _context;
    public AccountRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }


}