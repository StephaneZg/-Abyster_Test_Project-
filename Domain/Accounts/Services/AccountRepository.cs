
using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.Domain.Accounts.Services;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    private DatabaseContext _context;
    public AccountRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public override IQueryable<Account> FindAll(bool trackChange){
        var accounts =  _context.Accounts.Include(acc => acc.user);
        return accounts;
    }

    public override IQueryable<Account> FindByCondition(Expression<Func<Account, bool>> expression, bool trackChanges){
        var accountJournals =  _context.Accounts.Where(expression)
                                            .Include(acc => acc.user);
        return accountJournals;
    }

}