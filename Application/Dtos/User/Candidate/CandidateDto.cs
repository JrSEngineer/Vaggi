using Domain.Enums;

namespace Application.Dtos.User.Candidate;
public sealed record CandidateDto(
    Guid Id,
    AccountType AccountType,
    string FullName,
    string Document,
    string Email,
    string Phone,
    string? ProfileImage,
    ProfileType ProfileType
);
