namespace Domain.Interfaces;

public interface IFormSchema
{
    Guid Id { get; set; }
    string Version { get; set; }
    string Title { get; set; }
    List<IFormField> Fields { get; set; }
}