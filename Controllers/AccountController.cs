
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
public class AccountController : ControllerBase {

    private IMediator _mediator;

    public AccountController(IMediator mediator){
        _mediator = mediator;
    }

    [HttpPost("credite")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<AccountDto>> CreditAccount(CreditAccountRequest creditAccountRequest){

        var command = new CreditAccountCommand(creditAccountRequest);

        try{
            var commandResult = await _mediator.Send(command);
            if(commandResult == true) return Ok("operation completed successfully");
            else return BadRequest();
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("debite")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> DebiteAccount(DebiteAccountRequest debiteAccountRequest){
        
        var command = new DebiteAccountCommand(debiteAccountRequest);

        try{
            var commandResult = await _mediator.Send(command);
            if(commandResult == true) return Ok("operation completed successfully");
            else return BadRequest();
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }

    }

    
}