
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.Domain.Account_Journals.Services;
using Abyster_Test_Project.Domain.Accounts.Services;
using Abyster_Test_Project.Domain.Roles.Services;
using Abyster_Test_Project.Domain.Users.Services;

namespace Abyster_Test_Project.Services;

public class ServiceManager : IServiceManager{

    private DatabaseContext _context;

    private IUserRepository _userRepository;

    private IAccountRepository _accountRepository;

    private ICategoryRepository _categoryRepository;

    private IAccountJournalRepository _accountJournalRepository;
    
    private IRoleRepository _roleRepository;

    public ServiceManager(DatabaseContext context){
        _context = context;
    }

    public IUserRepository User
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }
            return _userRepository;
        }
    }

    public IAccountRepository Account  {
        get
        {
            if (_accountRepository == null)
            {
                _accountRepository = new AccountRepository(_context);
            }
            return _accountRepository;
        }
    }

    public ICategoryRepository Category 
    {
        get
        {
            if (_categoryRepository == null)
            {
                _categoryRepository = new CategoryRepository(_context);
            }
            return _categoryRepository;
        }
    }

    public IAccountJournalRepository AccountJournal{
        get
        {
            if (_accountJournalRepository == null)
            {
                _accountJournalRepository = new AccountJournalRepository(_context);
            }
            return _accountJournalRepository;
        }
    }

    public IRoleRepository Role {
        get
        {
            if (_roleRepository == null)
            {
                _roleRepository = new RoleRepository(_context);
            }
            return _roleRepository;
        }
    }

    public async Task Save(){
        await _context.SaveChangesAsync();
    }
}