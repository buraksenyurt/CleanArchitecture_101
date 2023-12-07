using FluentValidation.Results;

namespace GamersWorld.Application.Common.Exceptions;

public class ValidationException
    : Exception
{
    public IDictionary<string, string[]> Errors { get; }
    public ValidationException()
    {
        Errors = new Dictionary<string, string[]>();
    }
    public ValidationException(IEnumerable<ValidationFailure> errors)
        : this()
    {
        var errorGroups = errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage);
        foreach (var e in errorGroups)
        {
            Errors.Add(e.Key, e.ToArray());
        }
    }
}