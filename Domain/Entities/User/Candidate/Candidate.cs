using Domain.Entities.Base;
using Domain.Entities.Jobs;
using Domain.Enums;

namespace Domain.Entities.User.Candidate;

public class Candidate : BaseEntity
{
    public ProfileType ProfileType { get; set; } = ProfileType.Basic;

    private readonly List<Application> _Applications = new List<Application>();
    public IReadOnlyCollection<Application> Applications => _Applications;
}
