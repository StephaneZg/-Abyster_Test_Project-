
using Abyster_Test_Project.Domain.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abyster_Test_Project.Domain.Users.Features.AuthenticateUser;
using static Abyster_Test_Project.Domain.Users.Features.DeactivateUser;
using static Abyster_Test_Project.Domain.Users.Features.GetAllUser;
using static Abyster_Test_Project.Domain.Users.Features.RegisterUser;

namespace Abyster_Test_Project.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase {

    private IMediator _mediator;

    public UserController(IMediator mediator){
        _mediator = mediator;
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser(AuthenticationRequest authRequest){

        var command = new AuthenticateUserCommand(authRequest);
        try{
            var commandReponse = _mediator.Send(command);
            return Ok(commandReponse);
        }catch(Exception e){
            return BadRequest(e.Message);
        }

    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> RegisterUser(RegistrationRequest registrationRequest){
        
        var command = new RegisterUserCommand(registrationRequest);
        try{
            var commandResponse = await _mediator.Send(command);
        }catch(Exception e){
            return BadRequest(e.Message);
        }

        return Ok("User registred successfully");
    }

    [HttpPost("deactivate/{user_id}")]
    [Authorize()]
    public async Task<ActionResult> DeactivateUser(int user_id){
        
        var command = new DeactivateUserCommand(user_id);
        try{
            var commandResult = await _mediator.Send(command);
            return NoContent();
        }catch(Exception e){
             return BadRequest(e.Message);
        }
    }



    [HttpGet("all")]
    [Authorize()]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(){
        var query = new UserListQuery();
        try{
            var queryResult = await _mediator.Send(query);

            if(queryResult.Count() == 0){
                return NotFound();
            }
            return Ok(queryResult);
        }catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}