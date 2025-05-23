namespace Application.Dtos.Jobs;

internal sealed record  NewInterviewDto(
    string Name,
    string Publisher,
    string CompanyField,
    string InterviewLocation
    );
