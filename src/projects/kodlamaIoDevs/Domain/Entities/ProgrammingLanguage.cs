namespace Domain.Entities;

public class ProgrammingLanguage : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Technology> Technologies { get; set; }
}