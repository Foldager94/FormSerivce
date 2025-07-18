namespace Domain.Interfaces;

public interface IFieldValidation
{
    bool Required { get; }
    int? MinLength { get; }
    int? MaxLength { get; }
    string? Pattern { get; }
}