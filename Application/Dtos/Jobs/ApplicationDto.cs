using Application.Dtos.User.Company;
using System.Collections.ObjectModel;

namespace Application.Dtos.Jobs;

public record ApplicationDto(
    Guid Id,
    CompanyDto Company,
    ReadOnlyCollection<VacancyMessageDto> Messages
);
