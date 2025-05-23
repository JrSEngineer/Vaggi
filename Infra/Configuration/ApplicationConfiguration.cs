using Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vaggi.Domain.Configuration;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasKey(vA => vA.Id);

        builder.HasIndex(vA => new { vA.VacancyId, vA.CandidateId }).IsUnique();

        builder.HasMany(vA => vA.Messages)
                .WithOne()
                .HasForeignKey(m => m.ApplicationId);

        builder.HasMany(vA => vA.Interviews)
                .WithOne()
                .HasForeignKey(m => m.ApplicationId);
    }
}
