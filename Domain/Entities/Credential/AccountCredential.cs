namespace Domain.Entities.Credential;

public class AccountCredential
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateTime LastLogin { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string? SecurityCode { get; set; }

    public void ModifyPassword(string newPasswrd)
    {
        Password = newPasswrd;
    }

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
