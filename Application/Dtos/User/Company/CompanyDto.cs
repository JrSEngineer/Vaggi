using Domain.Enums;

namespace Application.Dtos.User.Company;

public sealed record CompanyDto(
    Guid Id,
    AccountType AccountType,
    string FullName,
    string Document,
    string Email,
    string Phone,
    string? ProfileImage
);
