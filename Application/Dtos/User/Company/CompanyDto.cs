using Domain.Enums;

namespace Application.Dtos.User.Company;

public record CompanyDto(
    Guid Id,
    AccountType AccountType,
    string FullName,
    string Document,
    string Email,
    string Phone,
    string? ProfileImage
);
