using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ProgrammingLanguages;

public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.HasQueryFilter(entity => !entity.IsDeleted);

        builder.HasMany(prop => prop.Technologies).WithOne(prop => prop.ProgrammingLanguage);
    }
}