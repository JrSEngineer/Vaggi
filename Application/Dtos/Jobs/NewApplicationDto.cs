using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Jobs;

public sealed record NewApplicationDto
{
    public Guid CandidateId { get; init; }
    public Guid VacancyId { get; init; }
    public IFormFile File { get; init; } = null!;
}
