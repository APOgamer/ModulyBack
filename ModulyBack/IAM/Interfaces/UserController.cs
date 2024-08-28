using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.IAM.Domain.Model.Queries;
using ModulyBack.IAM.Domain.Services;
using ModulyBack.IAM.Interfaces.Transform;

namespace ModulyBack.IAM.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController(IUserQueryServices userQueryServices, IUserCommandService userCommandService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryServices.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    [HttpGet("getbyusername/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var getUserByIdQuery = new GetUserByNameQuery(username);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }


    [HttpGet("getinformationbyusername/{username}")]
    public async Task<IActionResult> GetInformationByUserName(string username)
    {
        var getUserByIdQuery = new GetUserByNameQuery(username);
        var user = await userQueryServices.Handle(getUserByIdQuery);
        var userResource = UserInformationResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    [HttpPut("{username}")]
    public async Task<IActionResult> UpdateUser(string username, [FromBody] User user)
    {
        if (username != user.Username)
        {
            return BadRequest("User ID mismatch");
        }

        try
        {
            await userCommandService.UpdateUser(user);
            return NoContent(); // 204 No Content
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // O maneja el error de otra manera según tu lógica de negocio
        }
    }
}
    
