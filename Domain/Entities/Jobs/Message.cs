namespace Domain.Entities.Jobs;

public class Message
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public DateTime SendedAt { get; set; }
    public string Content { get; set; } = string.Empty;
}
