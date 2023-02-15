
using System.Linq.Expressions;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.Domain.Account_Journals.Services;

public class AccountJournalRepository : RepositoryBase<AccountJournal>, IAccountJournalRepository {

    private DatabaseContext _context;
    public AccountJournalRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public override IQueryable<AccountJournal> FindAll(bool trackChange){
        var accountJournals =  _context.AccountJournals.Include(acc => acc.account)
                                            .Include(acc => acc.category)
                                            .Include(acc => acc.user);
        return accountJournals;
    }

    public override IQueryable<AccountJournal> FindByCondition(Expression<Func<AccountJournal, bool>> expression, bool trackChanges){
        var accountJournals =  _context.AccountJournals.Where(expression)
                                            .Include(acc => acc.account)
                                            .Include(acc => acc.category)
                                            .Include(acc => acc.user);
        return accountJournals;
    }

}