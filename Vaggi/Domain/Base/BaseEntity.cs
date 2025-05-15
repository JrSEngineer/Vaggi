namespace Vaggi.Domain.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? ProfileImage { get; set; }
}
