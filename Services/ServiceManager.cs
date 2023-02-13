
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Data;
using Abyster_Test_Project.Domain.Users.Services;

namespace Abyster_Test_Project.Services;

public class ServiceManager : IServiceManager{

    private DatabaseContext _context;

    private IUserRepository _userRepository;

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

    public async Task Save(){
        await _context.SaveChangesAsync();
    }
}