using Microsoft.AspNetCore.Mvc;
using ModulyBack.IAM.Application.Internal.OutboundServices;
using ModulyBack.IAM.Domain.Services;
using ModulyBack.IAM.Interfaces.Resources;
using ModulyBack.IAM.Interfaces.Transform;
namespace ModulyBack.IAM.Interfaces;

public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly ITokenService _tokenService;
    private readonly IUserCommandService userCommandService;
    public AuthenticationController(IUserCommandService userCommandService)
    {
        _userCommandService = userCommandService ?? throw new ArgumentNullException(nameof(userCommandService));
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        try
        {
            var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);

            var (authenticatedUser, token) = await _userCommandService.Handle(signInCommand);

            if (authenticatedUser == null || string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Return token and any additional info
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        try
        {
            var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
            await _userCommandService.Handle(signUpCommand);
            return Ok(new { message = "User created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }
}