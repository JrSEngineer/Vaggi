namespace Applicatio.Dtos.Credential;

internal sealed record AccessDto(
    Guid OwnerId,
    string AccessToken,
    string RefreshToken
    );
