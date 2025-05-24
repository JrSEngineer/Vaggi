using Application.Dtos.Jobs;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Vaggi.Infra.Context;

namespace Infrastructure.Repositories;

public class VacanciesRepository : IVacanciesRepository
{
    private readonly AppDbContext _context;

    public VacanciesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VacancyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Vacancy
                .AsNoTracking()
                .Include(v => v.Company)
                .Select(v => v.ToDto())
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao listar as vagas.", ex);
        }
    }

    public async Task<VacancyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var vacancy = await _context.Vacancy.SingleOrDefaultAsync(v => v.Id == id, cancellationToken);
            if (vacancy is null) throw new Exception("Vaga não encontrada.");
            return vacancy.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar a vaga.", ex);
        }
    }

    public async Task<VacancyDto> AddAsync(NewVacancyDto dto, CancellationToken cancellationToken)
    {
        try
        {
            bool CompanyFound = await _context.Company.AnyAsync(c => c.Id == dto.CompanyId, cancellationToken);

            if (!CompanyFound) throw new Exception("Erro ao adicionar vaga. Empresa não encontrada.");

            var entity = dto.ToEntity();

            await _context.Vacancy.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return (await GetByIdAsync(entity.Id, cancellationToken))!;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao adicionar a vaga.", ex);
        }
    }

    public async Task<VacancyDto> UpdateAsync(Guid id, UpdateVacancyDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var vacancy = await _context.Vacancy.SingleOrDefaultAsync(v => v.Id == id, cancellationToken);
            if (vacancy is null) throw new Exception("Vaga não encontrada.");

            vacancy.Name = dto.Name;
            vacancy.CompanyField = dto.CompanyField;
            vacancy.Description = dto.Description;
            vacancy.InterviewLocation = dto.InterviewLocation;
            vacancy.InterviewDate = dto.InterviewaDate;

            await _context.SaveChangesAsync(cancellationToken);

            return vacancy.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar a vaga.", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var vacancy = await _context.Vacancy.SingleOrDefaultAsync(v => v.Id == id, cancellationToken);
            if (vacancy is null) throw new Exception("Vaga não encontrada.");

            _context.Vacancy.Remove(vacancy);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao excluir a vaga.", ex);
        }
    }
}
