using Application.Dtos.Jobs;
using Domain.Entities.Jobs;

namespace Application.Extensions;

public static class VacancyExtensions
{
    public static VacancyDto ToDto(this Vacancy vacancy)
    {
        return new VacancyDto(
            vacancy.Id,
            vacancy.Company!.ToDto(),
            vacancy.Name,
            vacancy.Publisher,
            vacancy.CompanyField,
            vacancy.Description,
            vacancy.InterviewLocation,
            vacancy.InterviewDate,
            vacancy.Status
        );
    }

    public static Vacancy ToEntity(this NewVacancyDto dto)
    {
        return new Vacancy
        {
            Id = Guid.NewGuid(),
            CompanyId = dto.CompanyId,
            Name = dto.Name,
            Publisher = dto.Publisher,
            CompanyField = dto.CompanyField,
            Description = dto.Description,
            InterviewLocation = dto.InterviewLocation,
            InterviewDate = dto.InterviewaDate
        };
    }
}
