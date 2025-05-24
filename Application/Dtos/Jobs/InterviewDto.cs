namespace Application.Dtos.Jobs;

public sealed record InterviewDto(
    Guid Id,
    string Name,
    string Publisher,
    string CompanyField,
    DateTime InterviewDate,
    string InterviewLocation
    );
