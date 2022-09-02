namespace Application.Features.ProgrammingLanguages.Constants;

public static class Messages
{
    public static string ProgrammingLanguage = "Programming Language";

    public static string AlreadyExists = "Already exists.";
    public static string NotFound = "Not found.";

    public static string Join(params string[] messages) => string.Join(" ", messages);
}