using Domain.Interfaces;

namespace Domain.Models;

public class FormSchema : IFormSchema
{
    public required Guid Id { get; set; }
    public required string Version { get; set; }
    public required string Title { get; set; }
    public List<IFormField> Fields { get; set; } = [];
}