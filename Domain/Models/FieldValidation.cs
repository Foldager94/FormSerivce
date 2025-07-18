using Domain.Interfaces;

namespace Domain.Models;

public class FieldValidation : IFieldValidation
{
    public bool Required { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string? Pattern { get; set; }
}