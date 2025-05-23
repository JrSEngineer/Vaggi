using Domain.Entities.User.Candidate;

namespace Domain.Entities.Jobs;

public class Application
{
    public Guid Id { get; set; }
    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    public Guid VacancyId { get; set; }
    public Vacancy Vacancy { get; set; } = null!;

    private readonly List<Message> _Messages = new List<Message>();
    public IReadOnlyCollection<Message> Messages => _Messages;
    
    private readonly List<Interview> _Interviews = new List<Interview>();
    public IReadOnlyCollection<Interview> Interviews => _Interviews;
}
