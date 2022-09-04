using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ProgrammingLanguages;

public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Name).IsRequired(true);
        builder.HasQueryFilter(entity => !entity.IsDeleted);
    }
}