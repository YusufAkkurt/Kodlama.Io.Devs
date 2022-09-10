using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Technologies;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.HasQueryFilter(prop => !prop.IsDeleted);

        builder.HasOne(prop => prop.ProgrammingLanguage).WithMany(prop => prop.Technologies);
    }
}