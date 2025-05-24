namespace Application.Dtos.User.Company;

public sealed record UpdateCompanyDto(
    string FullName,
    string Document,
    string Email,
    string Phone
);
