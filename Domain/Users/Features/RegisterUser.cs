
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;
using MediatR;
using BCrypt.Net;

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
            userToSave.password = BCrypt.HashPasword()
            _serviceManager.User.Create((User) userToSave);
            await _serviceManager.Save();
            return true;
        }
    }
}