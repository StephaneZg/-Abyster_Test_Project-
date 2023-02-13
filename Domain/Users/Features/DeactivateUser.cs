
using Abyster_Test_Project.Contract;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Users.Features;

public class DeactivateUser{

    public class DeactivateUserCommand : IRequest<bool> {
        
        public readonly int userId;

        public DeactivateUserCommand(int userId){
            this.userId = userId;
        }
    }

    public class Handler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }


        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _serviceManager.User.FindByCondition(user =>
                    user.Id == request.userId, false).SingleOrDefault();
            if(user != null ){
                User matchUser = (User) user;
                matchUser.isActive = false;
                _serviceManager.User.Update(matchUser);
                await _serviceManager.Save();
                return true;
            }
            return false;
        }
    }
}