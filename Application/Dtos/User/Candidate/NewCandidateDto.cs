namespace Application.Dtos.User.Candidate;

public sealed record NewCandidateDto(
    string FullName,
    string Document,
    string Email,
    string Password,
    string Phone
);
