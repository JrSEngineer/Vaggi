using Application.Dtos.Credential;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly ISecurityRepository _repository;

    public SecurityController(ISecurityRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var access = await _repository.LoginAsync(dto, cancellationToken);
        return Ok(access);
    }

    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword(RecoveryDto dto, CancellationToken cancellationToken)
    {
        await _repository.RecoverPasswordAsync(dto, cancellationToken);
        return Ok(new { Message = "Código de recuperação enviado." });
    }
}
