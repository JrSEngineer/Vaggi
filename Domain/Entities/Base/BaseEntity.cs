using Domain.Entities.Credential;
using Domain.Entities.Jobs;
using Domain.Enums;

namespace Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public AccountType AccountType { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? ProfileImage { get; set; }
    public AccountCredential Credential { get; set; } = null!;

    private readonly List<Vacancy> _Vacancies = new List<Vacancy>();
    public IReadOnlyCollection<Vacancy> Vacancies => _Vacancies;
}
