using Domain.Interfaces;

namespace Domain.Models;

public class TextField : IFormField
{
    public string Key { get; set; }
    public string Type{ get; set; }
    public string Label { get; set; }
    public string Placeholder { get; set; }
    public IFieldValidation Validation { get; set; }
    public bool Validate(object value)
    {
        throw new NotImplementedException();
    }
}