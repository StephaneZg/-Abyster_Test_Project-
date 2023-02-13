
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.SharedKernel;

namespace Abyster_Test_Project.Domain.Account_Journals.Services;

public class AccountJournalRepository : RepositoryBase<AccountJournal>, IAccountJournalRepository {

    private DatabaseContext _context;
    public AccountJournalRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

}