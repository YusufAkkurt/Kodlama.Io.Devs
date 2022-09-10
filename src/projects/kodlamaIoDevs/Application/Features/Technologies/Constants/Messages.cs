namespace Application.Features.Technologies.Constants;

public static class Messages
{
    public static string Technology = "Technology";

    public static string AlreadyExists = "Already exists.";
    public static string NotFound = "Not found.";
    public static string NotExists = "Not exists.";

    public static string Join(params string[] messages) => string.Join(" ", messages);
}