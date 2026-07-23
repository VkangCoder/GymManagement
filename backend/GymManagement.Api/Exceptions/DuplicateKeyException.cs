namespace GymManagement.Api.Exceptions;

public class DuplicateKeyException : Exception
{

    public string? ConstraintName { get; }

    public DuplicateKeyException(string? constraintName, Exception? inner = null)
        : base($"Duplicate value violates constraint: {constraintName}", inner)
    {
        ConstraintName = constraintName;
    }
}