
using Abyster_Test_Project.Domain.Users.Dtos;
using Abyster_Test_Project.Domain.Users.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abyster_Test_Project.Domain.Users.Features.AuthenticateUser;
using static Abyster_Test_Project.Domain.Users.Features.DeactivateUser;
using static Abyster_Test_Project.Domain.Users.Features.GetAllUser;
using static Abyster_Test_Project.Domain.Users.Features.RegisterAdminUser;
using static Abyster_Test_Project.Domain.Users.Features.RegisterUser;

namespace Abyster_Test_Project.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{

    private IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser(AuthenticationRequest authRequest)
    {

        var command = new AuthenticateUserCommand(authRequest);

        var commandReponse = _mediator.Send(command);
        return Ok(commandReponse);


    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> RegisterUser(RegistrationRequest registrationRequest)
    {

        var command = new RegisterUserCommand(registrationRequest);

        var commandResponse = await _mediator.Send(command);


        return Ok("User registred successfully");
    }

    [HttpPost("register/admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RegisterAdminUser(RegistrationRequest registrationRequest)
    {

        var command = new RegisterUserAdminCommand(registrationRequest);

        var commandResponse = await _mediator.Send(command);


        return Ok("User registred successfully");
    }

    [HttpPost("deactivate/{user_id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeactivateUser(int user_id)
    {

        var command = new DeactivateUserCommand(user_id);
        var commandResult = await _mediator.Send(command);
        return NoContent();

    }



    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var query = new UserListQuery();
        try
        {
            var queryResult = await _mediator.Send(query);

            if (queryResult.Count() == 0)
            {
                return NotFound();
            }
            return Ok(queryResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}