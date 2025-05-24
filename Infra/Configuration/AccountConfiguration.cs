using Domain.Entities.Credential;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class AccountConfiguration : IEntityTypeConfiguration<AccountCredential>
{
    public void Configure(EntityTypeBuilder<AccountCredential> builder)
    {
        builder.HasKey(cr => cr.Id);
        builder.HasIndex(cr => cr.Email).IsUnique();
    }
}
