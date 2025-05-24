using Applicatio.Dtos.Credential;
using Application.Dtos.Credential;
using Application.Interfaces.Repositories;
using Infra.Services.Security;
using Infra.Services.TokenService;
using Microsoft.EntityFrameworkCore;
using Vaggi.Infra.Context;

namespace Infrastructure.Repositories;

public class SecurityRepository : ISecurityRepository
{
    private readonly AppDbContext _context;
    private readonly HashService _hashService;
    private readonly TokenService _tokenService;

    public SecurityRepository(AppDbContext context, HashService hashService, TokenService tokenService)
    {
        _context = context;
        _hashService = hashService;
        _tokenService = tokenService;
    }

    public async Task<AccessDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var credential = await _context.AccountCredential
            .FirstOrDefaultAsync(c => c.Email == dto.Email, cancellationToken);

            if (
                credential is null ||
                !_hashService.VerifyPassword(dto.Password, credential.Password))
                throw new Exception("Credenciais inválidas! Verifique seus dados.");

            string accessToken = _tokenService.CreateToken(credential);
            string refreshToken = _tokenService.GenerateRefreshToken(credential);

            credential.LastLogin = DateTime.UtcNow;
            credential.SetRefreshToken(refreshToken);

            await _context.SaveChangesAsync(cancellationToken);

            var accessDto = new AccessDto(
                credential.AccountId,
                accessToken,
                refreshToken
                );


            return accessDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao autenticar.", ex);
        }
    }

    public async Task RecoverPasswordAsync(RecoveryDto dto, CancellationToken cancellationToken)
    {
        try
        {
            //TODO: Implement code sending to user email.

            var credential = await _context.AccountCredential
            .FirstOrDefaultAsync(c => c.Email == dto.Email, cancellationToken);

            if (credential is null) throw new Exception("Email inválido.");

            credential.SecurityCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao enviar código de segurança.", ex);
        }
    }
}
