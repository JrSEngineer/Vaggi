using Domain.Entities.Base;
using Domain.Entities.User.Candidate;
using Domain.Entities.User.Company;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

public class BaseConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasDiscriminator<AccountType>(x => x.AccountType)
               .HasValue<Company>(AccountType.Company)
               .HasValue<Candidate>(AccountType.Candidate);

    }
}
