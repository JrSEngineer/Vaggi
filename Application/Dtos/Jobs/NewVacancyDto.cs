namespace Application.Dtos.Jobs;

internal sealed record  NewVacancyDto(
    Guid CompanyId,
    string Name,
    string Publisher,
    string CompanyField,
    string Description,
    string InterviewLocation,
    DateTime InterviewaDate
);
