
using System.IO.Compression;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Accounts;
using Abyster_Test_Project.Domain.Roles;
using Abyster_Test_Project.Domain.Users.Dtos;
using Abyster_Test_Project.Service.Contract;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Abyster_Test_Project.Domain.Users.Features;

public class RegisterAdminUser{

    public class RegisterUserAdminCommand : IRequest<bool>{

        public readonly RegistrationRequest registrationRequest;

        public RegisterUserAdminCommand(RegistrationRequest registrationRequest){

            this.registrationRequest = registrationRequest;
        }
    }

    public class Handler : IRequestHandler<RegisterUserAdminCommand, bool>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        private ICurrentUserService _currentUserService;

        public Handler(IServiceManager serviceManager, IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(RegisterUserAdminCommand request, CancellationToken cancellationToken)
        {
            RegistrationRequest registrationRequest = request.registrationRequest;

            if(_currentUserService.UserId == null){
                throw new Exception("Some information are missing");
            }
            int requestInitUserId = int.Parse(_currentUserService.UserId);
            var matchRequestInitUserIdInDb =  _serviceManager.User.FindByCondition(user => 
                                user.Id == requestInitUserId, false).SingleOrDefault();
            if(matchRequestInitUserIdInDb == null){
                throw new Exception("Request init user have been deleted");
            }
            if(matchRequestInitUserIdInDb.initialized == false){
                throw new Exception("User not Authorize to perform those action");
            }

            var user =  _serviceManager.User.FindByCondition(user => 
                                user.emailAddress == registrationRequest.emailAddress, false).SingleOrDefault();
            if(user != null){
                throw new Exception("User is already registered");
            }
            
            User userToSave = _mapper.Map<User>(registrationRequest);
            userToSave.password = BCrypt.Net.BCrypt.HashPassword(userToSave.password);
            userToSave.roles = new List<Role>(){getAdminRole()};
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
            var matchRole = _serviceManager.Role.FindByCondition(role => role.libelle == "User", true).SingleOrDefault();
            if (matchRole == null)
            {
                throw new Exception(message: "Category does not exists.");
            }
            return matchRole;
        }

        private Role getAdminRole()
        {
            var matchRole = _serviceManager.Role.FindByCondition(role => role.libelle == "Admin", true).SingleOrDefault();
            if (matchRole == null)
            {
                throw new Exception(message: "Category does not exists.");
            }
            return matchRole;
        }
    }
}