using Application.Dtos.Jobs;

namespace Application.Interfaces.Repositories;

public interface IVacanciesRepository
{
    Task<List<VacancyDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<VacancyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<VacancyDto> AddAsync(NewVacancyDto dto, CancellationToken cancellationToken);
    Task<VacancyDto> UpdateAsync(Guid id, UpdateVacancyDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
