using Domain.Entities.Credential;
using Domain.Entities.User.Company;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vaggi.Domain.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Credential)
               .WithOne()
               .HasForeignKey<AccountCredential>(cr => cr.AccountId);

        builder.HasMany(c => c.Vacancies)
               .WithOne()
               .HasForeignKey(v => v.CompanyId);

        builder.Property(c => c.AccountType)
               .HasConversion(
               a => a.ToString(),
               a => (AccountType)Enum.Parse(typeof(AccountType), a));
    }
}
