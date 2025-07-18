namespace Domain.Interfaces;

public interface IFormField
{
    string Key { get; }
    string Type { get; }
    string Label { get; }
    string Placeholder { get; }
    
    IFieldValidation Validation { get; }
    
    bool Validate(object value);
}