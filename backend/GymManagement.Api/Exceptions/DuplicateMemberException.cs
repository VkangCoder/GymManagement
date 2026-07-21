namespace GymManagement.Api.Exceptions;

public class DuplicateMemberException : Exception
{
    public string Field { get; }

    public DuplicateMemberException(string field)
        : base($"A member with the same {field} already exists.")
    {
        Field = field;
    }
}