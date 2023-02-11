
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Users.Dtos;
using AutoMapper;
using MediatR;

namespace Abyster_Test_Project.Domain.Users.Features;


public class GetAllUser{

    public class UserListQuery : IRequest<IEnumerable<UserDto>>{

    }

    public class Handler : IRequestHandler<UserListQuery, IEnumerable<UserDto>>
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        
        public async Task<IEnumerable<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> allUsers = _serviceManager.User.FindAll(false);
            IEnumerable<UserDto> allUserToReturn = new List<UserDto>();
            if(allUsers.Count() != 0) {
                allUserToReturn = _mapper.Map<IEnumerable<UserDto>>(allUsers); 
            }
            return allUserToReturn;
        }
    }
}