
using Abyster_Test_Project.Domain.Accounts.Dto;
using Abyster_Test_Project.Domain.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abyster_Test_Project.Domain.Accounts.Features.CreditAccount;
using static Abyster_Test_Project.Domain.Accounts.Features.DebiteAccount;
using static Abyster_Test_Project.Domain.Users.Features.AuthenticateUser;
using static Abyster_Test_Project.Domain.Users.Features.DeactivateUser;
using static Abyster_Test_Project.Domain.Users.Features.GetAllUser;
using static Abyster_Test_Project.Domain.Users.Features.RegisterUser;

namespace Abyster_Test_Project.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{

    private IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("credite")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<AccountDto>> CreditAccount(CreditAccountRequest creditAccountRequest)
    {

        var command = new CreditAccountCommand(creditAccountRequest);


        var commandResult = await _mediator.Send(command);
        if (commandResult == true) return Ok("operation completed successfully");
        else return BadRequest();


    }

    [HttpPost("debite")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DebiteAccount(DebiteAccountRequest debiteAccountRequest)
    {

        var command = new DebiteAccountCommand(debiteAccountRequest);

        var commandResult = await _mediator.Send(command);
        if (commandResult == true) return Ok("operation completed successfully");
        else return BadRequest();
    }


}