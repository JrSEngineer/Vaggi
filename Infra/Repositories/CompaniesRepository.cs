using Applicatio.Dtos.Credential;
using Application.Dtos.User.Company;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Domain.Entities.Credential;
using Domain.Entities.User.Candidate;
using Infra.Services.Security;
using Infra.Services.TokenService;
using Microsoft.EntityFrameworkCore;
using Vaggi.Infra.Context;

namespace Infrastructure.Repositories;

public class CompaniesRepository : ICompaniesRepository
{

    private readonly AppDbContext _context;
    private readonly HashService _hashService;
    private readonly TokenService _tokenService;


    public CompaniesRepository(AppDbContext context, HashService hashService, TokenService tokenService)
    {
        _context = context;
        _hashService = hashService;
        _tokenService = tokenService;
    }

    public async Task<List<CompanyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Company
                .AsNoTracking()
                .Select(company => company.ToDto())
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao listar empresas.", ex);
        }
    }

    public async Task<CompanyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _context.Company
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (company is null) throw new Exception("Empresa não encontrada.");

            return company.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar empresa.", ex);
        }
    }

    public async Task<AccessDto> AddAsync(NewCompanyDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var company = dto.ToEntity();

            var credential = new AccountCredential
            {
                Id = Guid.NewGuid(),
                AccountId = company.Id,
                Email = dto.Email,
                Password = dto.Password,
                LastLogin = DateTime.UtcNow,
                RefreshToken = string.Empty,
            };

            string accessToken = _tokenService.CreateToken(credential);
            string refreshToken = _tokenService.GenerateRefreshToken(credential);

            string hashedPassword = _hashService.HashPassword(dto.Password);
            credential.ModifyPassword(hashedPassword);
            credential.SetRefreshToken(refreshToken);

            await _context.Company.AddAsync(company, cancellationToken);
            await _context.AccountCredential.AddAsync(credential, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var accessDto = new AccessDto(
             company.Id,
             accessToken,
             refreshToken
             );

            return accessDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao adicionar empresa.", ex);
        }
    }

    public async Task<CompanyDto> UpdateAsync(Guid id, UpdateCompanyDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _context.Company.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (company is null) throw new Exception("Empresa não encontrada.");

            company.FullName = dto.FullName;
            company.Document = dto.Document;
            company.Email = dto.Email;
            company.Phone = dto.Phone;

            await _context.SaveChangesAsync(cancellationToken);

            return company.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar empresa.", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _context.Company.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (company is null) throw new Exception("Empresa não encontrada.");

            _context.Company.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao excluir empresa.", ex);
        }
    }
}
