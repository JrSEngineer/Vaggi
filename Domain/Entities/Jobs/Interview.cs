namespace Domain.Entities.Jobs;

public class Interview
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public DateTime InterviewDate { get; set; }
    public string InterviewLocation { get; set; } = string.Empty;
}
