
using Application.Dtos.User.Company;
using Domain.Enums;

namespace Application.Dtos.Jobs;

internal sealed record VacancyDto(
    Guid Id,
    CompanyDto Company,
    string Name,
    string Publisher,
    string CompanyField,
    string Description,
    string InterviewLocation,
    DateTime InterviewaDate,
    JobStatus Status
);
