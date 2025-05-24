namespace Applicatio.Dtos.Credential;

public sealed record AccessDto(
    Guid OwnerId,
    string AccessToken,
    string RefreshToken
    );
