namespace Application.Dtos.User.Company;

internal sealed record NewCompanyDto(
    string FullName,
    string Document,
    string Email,
    string Password,
    string Phone
);
