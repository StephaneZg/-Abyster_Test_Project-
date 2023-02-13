
using System.IO.Compression;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Accounts;
using Abyster_Test_Project.Domain.Roles;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Users.Features;

public class RegisterUser{

    public class RegisterUserCommand : IRequest<bool>{

        public readonly RegistrationRequest registrationRequest;

        public RegisterUserCommand(RegistrationRequest registrationRequest){

            this.registrationRequest = registrationRequest;
        }
    }

    public class Handler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            RegistrationRequest registrationRequest = request.registrationRequest;
            var user =  _serviceManager.User.FindByCondition(user => 
                                user.emailAddress == registrationRequest.emailAddress, false).SingleOrDefault();
            if(user != null){
                throw new Exception("User is already registered");
            }

            User userToSave = _mapper.Map<User>(registrationRequest);
            userToSave.password = BCrypt.Net.BCrypt.HashPassword(userToSave.password);
            userToSave.roles.Append(getUserRole());
            _serviceManager.User.Create((User) userToSave);
            await _serviceManager.Save();

            Account userAccount = new Account();
            userAccount.balance = 0;
            userAccount.user = userToSave;
            _serviceManager.Account.Create(userAccount);
            await _serviceManager.Save();

            return true;
        }

        private Role getUserRole()
        {
            var matchRole = _serviceManager.Role.FindByCondition(role => role.libelle == "User", false).SingleOrDefault();
            if (matchRole == null)
            {
                throw new Exception("Category does not exists.");
            }
            return matchRole;
        }
    }
}