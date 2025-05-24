using Applicatio.Dtos.Credential;
using Application.Dtos.User.Candidate;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Domain.Entities.Credential;
using Infra.Services.Security;
using Infra.Services.TokenService;
using Microsoft.EntityFrameworkCore;
using Vaggi.Infra.Context;

namespace Infrastructure.Repositories;

public class CandidatesRepository : ICandidatesRepository
{
    private readonly AppDbContext _context;
    private readonly HashService _hashService;
    private readonly TokenService _tokenService;

    public CandidatesRepository(AppDbContext context, HashService hashService, TokenService tokenService)
    {
        _context = context;
        _hashService = hashService;
        _tokenService = tokenService;
    }
    public async Task<List<CandidateDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Candidate
                .AsNoTracking()
                .Select(c => c.ToDto())
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao listar os usuários.", ex);
        }
    }

    public async Task<CandidateDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _context.Candidate.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (candidate is null) throw new Exception("Usuário não encontrado.");
            return candidate.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar o usuário.", ex);
        }
    }

    public async Task<AccessDto> AddAsync(NewCandidateDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = dto.ToEntity();

            var credential = new AccountCredential
            {
                Id = Guid.NewGuid(),
                AccountId = candidate.Id,
                Email = dto.Email,
                Password = dto.Password,
                LastLogin = DateTime.UtcNow.AddHours(-3),
                RefreshToken = string.Empty,
            };

            string accessToken = _tokenService.CreateToken(credential);
            string refreshToken = _tokenService.GenerateRefreshToken(credential);

            string hashedPassword = _hashService.HashPassword(dto.Password);
            credential.ModifyPassword(hashedPassword);
            credential.SetRefreshToken(refreshToken);

            await _context.Candidate.AddAsync(candidate, cancellationToken);
            await _context.AccountCredential.AddAsync(credential, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var accessDto = new AccessDto(
                candidate.Id,
                accessToken,
                refreshToken
                );

            return accessDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao adicionar o usuário.", ex);
        }
    }

    public async Task<CandidateDto> UpdateAsync(Guid id, UpdateCandidateDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _context.Candidate.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (candidate is null) throw new Exception("Usuário não encontrado.");

            candidate.ProfileType = dto.ProfileType;
            candidate.Email = dto.Email;
            candidate.Phone = dto.Phone;
            candidate.ProfileImage = dto.ProfileImage;

            await _context.SaveChangesAsync(cancellationToken);

            return candidate.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar o usuário.", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var candidate = await _context.Candidate.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (candidate is null) throw new Exception("Usuário não encontrado.");

            _context.Candidate.Remove(candidate);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao excluir o usuário.", ex);
        }
    }
}
