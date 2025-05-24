using Domain.Enums;

namespace Application.Dtos.User.Candidate;

public sealed record UpdateCandidateDto(
    ProfileType ProfileType,
    string Email,
    string Phone,
    string? ProfileImage
);
