using Applicatio.Dtos.Credential;
using Application.Dtos.User.Candidate;

namespace Application.Interfaces.Repositories;

public interface ICandidatesRepository
{
    Task<List<CandidateDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CandidateDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AccessDto> AddAsync(NewCandidateDto dto, CancellationToken cancellationToken);
    Task<CandidateDto> UpdateAsync(Guid id, UpdateCandidateDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
