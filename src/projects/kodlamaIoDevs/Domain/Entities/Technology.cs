namespace Domain.Entities;

public class Technology : BaseEntity
{
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }
}