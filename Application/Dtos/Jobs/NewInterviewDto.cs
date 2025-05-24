namespace Application.Dtos.Jobs;

public sealed record  NewInterviewDto(
    string Name,
    string Publisher,
    string CompanyField,
    string InterviewLocation
    );
