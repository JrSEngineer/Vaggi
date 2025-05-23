using Domain.Entities.Credential;
using Domain.Entities.User.Candidate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vaggi.Domain.Configuration;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Credential)
               .WithOne()
               .HasForeignKey<AccountCredential>(cr => cr.AccountId);

        builder.HasMany(v => v.Applications)
               .WithOne()
               .HasForeignKey(a => a.CandidateId);

        builder.Property(c => c.ProfileType)
               .HasConversion(
                c => c.ToString(),
                c => (ProfileType)Enum.Parse(typeof(ProfileType), c));

        builder.Property(c => c.AccountType)
               .HasConversion(
               a => a.ToString(),
               a => (AccountType)Enum.Parse(typeof(AccountType), a));
    }
}
