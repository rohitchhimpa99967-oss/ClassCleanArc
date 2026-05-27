using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllUsersQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(id));
        return ResponseHelper.GenerateResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CreateUserCommand command)
    {
        var response = await _mediator.Send(new UpdateUserCommand(id,command));
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteUserCommand(id));
        return Ok(response);
    }
}
