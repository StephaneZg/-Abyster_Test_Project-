
using Abyster_Test_Project.Domain.Account_Journals.Dto;
using Abyster_Test_Project.Domain.Accounts.Dto;
using Abyster_Test_Project.Domain.Categorys.Dto;
using Abyster_Test_Project.Domain.Categorys.Features;
using Abyster_Test_Project.Domain.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Abyster_Test_Project.Domain.Account_Journals.Features.GetAllUserOperations;
using static Abyster_Test_Project.Domain.Account_Journals.Features.GetUserOperations;
using static Abyster_Test_Project.Domain.Accounts.Features.CreditAccount;
using static Abyster_Test_Project.Domain.Accounts.Features.DebiteAccount;
using static Abyster_Test_Project.Domain.Categorys.Features.AddCategory;
using static Abyster_Test_Project.Domain.Categorys.Features.DeletCategory;
using static Abyster_Test_Project.Domain.Categorys.Features.GetAllCategories;
using static Abyster_Test_Project.Domain.Users.Features.AuthenticateUser;
using static Abyster_Test_Project.Domain.Users.Features.DeactivateUser;
using static Abyster_Test_Project.Domain.Users.Features.GetAllUser;
using static Abyster_Test_Project.Domain.Users.Features.RegisterUser;

namespace Abyster_Test_Project.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{

    private IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> AllCategories()
    {

        var query = new QueryListCategory();


        var commandResult = await _mediator.Send(query);
        return Ok(commandResult);


    }

    [HttpDelete("{user_id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCategory(int user_id)
    {

        var command = new DeleteCategoryCommand(user_id);

        var commandResult = await _mediator.Send(command);
        return NoContent();


    }

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddCategory(AddCategoryDto addCategoryDto)
    {

        var command = new AddCategoryCommand(addCategoryDto);


        var commandResult = await _mediator.Send(command);
        if (!(commandResult.GetType() == typeof(CategoryDto)))
        {
            return BadRequest("Something When wrong, please retry");
        }
        return Ok("Category successfully added");


    }

}