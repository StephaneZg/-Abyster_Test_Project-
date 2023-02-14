
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using Abyster_Test_Project.Domain.Accounts.Dto;
using Abyster_Test_Project.Domain.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abyster_Test_Project.Domain.Account_Journals.Features.GetAllUserOperations;
using static Abyster_Test_Project.Domain.Account_Journals.Features.GetUserOperations;
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
public class AccountjournalController : ControllerBase {

    private IMediator _mediator;

    public AccountjournalController(IMediator mediator){
        _mediator = mediator;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<AccountJournalDto>>> AllUserOperations(){

        var command = new QueryUserListOperations();

        try{
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("{user_id}")]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<IEnumerable<AccountJournalDto>>> UserOperations(int user_id){
        
        var command = new QueryUserOperations(user_id);

        try{
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }

    }

    
}