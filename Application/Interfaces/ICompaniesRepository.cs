using Applicatio.Dtos.Credential;
using Application.Dtos.User.Company;

namespace Application.Interfaces.Repositories;

public interface ICompaniesRepository
{
    Task<List<CompanyDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CompanyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AccessDto> AddAsync(NewCompanyDto dto, CancellationToken cancellationToken);
    Task<CompanyDto> UpdateAsync(Guid id, UpdateCompanyDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
