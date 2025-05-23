using Domain.Enums;

namespace Domain.Entities.Jobs;

public class Vacancy
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string CompanyField { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InterviewLocation { get; set; } = string.Empty;
    public DateTime InterviewDate { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Open;

    private readonly List<Application> _Applications = new List<Application>();
    public IReadOnlyCollection<Application> Applications => _Applications;
}
