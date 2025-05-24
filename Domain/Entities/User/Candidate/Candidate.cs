using Domain.Entities.Base;
using Domain.Entities.Jobs;
using Domain.Enums;

namespace Domain.Entities.User.Candidate;

public class Candidate : BaseEntity
{
    public ProfileType ProfileType { get; set; } = ProfileType.Basic;

    private readonly List<VacancyApplication> _Applications = new List<VacancyApplication>();
    public IReadOnlyCollection<VacancyApplication> Applications => _Applications;
}
