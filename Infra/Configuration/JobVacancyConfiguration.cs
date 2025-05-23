using Domain.Entities.Jobs;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vaggi.Domain.Configuration;

public class JobVacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasMany(v => v.Applications)
               .WithOne()
               .HasForeignKey(a => a.VacancyId);

        builder.Property(v => v.Status)
               .HasConversion(
                s => s.ToString(),
                s => (JobStatus)Enum.Parse(typeof(JobStatus), s));
    }
}
