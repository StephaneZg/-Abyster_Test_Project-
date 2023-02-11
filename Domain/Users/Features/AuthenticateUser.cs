
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Abyster_Test_Project.Contract;
using Abyster_Test_Project.Domain.Users.Dtos;
using Abyster_Test_Project.Services;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Abyster_Test_Project.Domain.Users.Features;

public class AuthenticateUser {

    public class AuthenticateUserCommand : IRequest<AuthenticationResponse>
    {
        public readonly AuthenticationRequest authRequest;

        public AuthenticateUserCommand(AuthenticationRequest authRequest)
        {
            this.authRequest = authRequest;
        }
    }

    public class Handler : IRequestHandler<AuthenticateUserCommand, AuthenticationResponse>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        private readonly ClaimsService _claimService;

        public Handler(IServiceManager serviceManager, IMapper mapper, IJwtTokenService jwtTokenService, ClaimsService claimsService)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
            _jwtTokenService = jwtTokenService;
            _claimService = claimsService;
        }

        public async Task<AuthenticationResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            AuthenticationRequest authRequest = request.authRequest;
            var user =  _serviceManager.User.FindByCondition(user => 
                                user.emailAddress == authRequest.emailAddress, false).SingleOrDefault();
            if(user == null){
                throw new Exception("User does not exists");
            }

            User matchUser = (User) user;
            List<Claim> userClaims = _claimService.GetUserClaimsAsync(matchUser);
            JwtSecurityToken token = _jwtTokenService.generateToken(userClaims);
            string refreshToken = _jwtTokenService.generateRefreshToken();

            matchUser.token = new JwtSecurityTokenHandler().WriteToken(token);
            matchUser.refreshToken = refreshToken;
            matchUser.refreshTokenExpireTime = DateTime.Now.AddDays(1);
            _serviceManager.User.Update(matchUser);

            AuthenticationResponse userResponse = _mapper.Map<AuthenticationResponse>(matchUser);

            return userResponse;
        }
    }
}