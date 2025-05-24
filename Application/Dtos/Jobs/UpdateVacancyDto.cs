namespace Application.Dtos.Jobs;

public sealed record UpdateVacancyDto(
    string Name,
    string Publisher,
    string CompanyField,
    string Description,
    string InterviewLocation,
    DateTime InterviewaDate
    );