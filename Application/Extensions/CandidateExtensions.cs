using Application.Dtos.User.Candidate;
using Domain.Entities.User.Candidate;
using Domain.Enums;

namespace Application.Extensions;

public static class CandidateExtensions
{
    public static CandidateDto ToDto(this Candidate candidate)
    {
        return new CandidateDto(
            Id: candidate.Id,
            AccountType: candidate.AccountType,
            ProfileType: candidate.ProfileType,
            FullName: candidate.FullName,
            Document: candidate.Document,
            Email: candidate.Email,
            Phone: candidate.Phone,
            ProfileImage: candidate.ProfileImage
        );
    }

    public static Candidate ToEntity(this NewCandidateDto dto)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            AccountType = AccountType.Candidate,
            ProfileType = ProfileType.Basic,
            FullName = dto.FullName,
            Document = dto.Document,
            Email = dto.Email,
            Phone = dto.Phone
        };
    }

    public static void Update(this Candidate candidate, UpdateCandidateDto dto)
    {
        candidate.ProfileType = dto.ProfileType;
        candidate.Email = dto.Email;
        candidate.Phone = dto.Phone;
        candidate.ProfileImage = dto.ProfileImage;
    }
}
