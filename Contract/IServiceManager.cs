
namespace Abyster_Test_Project.Contract;

public interface IServiceManager {

    IUserRepository User {get;}

    IAccountRepository Account {get;}

    ICategoryRepository Category {get;}

    IAccountJournalRepository AccountJournal {get;}

    IRoleRepository Role{get;}
    Task Save();
}