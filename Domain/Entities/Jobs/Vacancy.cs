using Domain.Entities.User.Company;
using Domain.Enums;

namespace Domain.Entities.Jobs;

public class Vacancy
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string CompanyField { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InterviewLocation { get; set; } = string.Empty;
    public DateTime InterviewDate { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Open;

    private readonly List<VacancyApplication> _Applications = new List<VacancyApplication>();
    public IReadOnlyCollection<VacancyApplication> Applications => _Applications;
}
