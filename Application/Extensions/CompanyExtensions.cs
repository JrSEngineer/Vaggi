using Application.Dtos.User.Company;
using Domain.Entities.User.Company;
using Domain.Enums;

namespace Application.Extensions;

public static class CompanyExtensions
{
    public static CompanyDto ToDto(this Company company)
    {
        return new CompanyDto(
            company.Id,
            company.AccountType,
            company.FullName,
            company.Document,
            company.Email,
            company.Phone,
            company.ProfileImage
        );
    }

    public static Company ToEntity(this NewCompanyDto dto)
    {
        return new Company
        {
            Id = Guid.NewGuid(),
            AccountType = AccountType.Company,
            FullName = dto.FullName,
            Document = dto.Document,
            Email = dto.Email,
            Phone = dto.Phone,
        };
    }
}
