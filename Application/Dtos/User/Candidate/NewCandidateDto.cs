namespace Application.Dtos.User.Candidate;

internal sealed record NewCandidateDto(
    string FullName,
    string Document,
    string Email,
    string Password,
    string Phone
);
