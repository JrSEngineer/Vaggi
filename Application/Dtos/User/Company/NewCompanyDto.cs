namespace Application.Dtos.User.Company;

public sealed record NewCompanyDto(
    string FullName,
    string Document,
    string Email,
    string Password,
    string Phone
);
