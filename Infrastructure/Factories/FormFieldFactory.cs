using System.Text.Json;
using Application.Factories;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Factories;

public class FormFieldFactory : IFormFieldFactory
{
    private readonly IFieldValidationFactory _validationFactory;
    private readonly Dictionary<string, Func<JsonElement, IFormField>> _creators;

    public FormFieldFactory(IFieldValidationFactory validationFactory)
    {
        _validationFactory = validationFactory;
        _creators = new Dictionary<string, Func<JsonElement, IFormField>>(StringComparer.OrdinalIgnoreCase)
        {
            { "text", CreateTextField }
        };
    }

    public IFormField Create(JsonElement json)
    {
        var type = json.GetProperty("type").GetString()
                   ?? throw new ArgumentNullException(nameof(json), "Type cannot be null");

        if (!_creators.TryGetValue(type, out var creator))
            throw new NotSupportedException($"Field type '{type}' is not supported.");

        return creator(json);
    }

    private IFormField CreateTextField(JsonElement json)
    {
        var key = json.GetProperty("key").GetString()!;
        var label = json.TryGetProperty("label", out var l) ? l.GetString() : "";
        var placeholder = json.TryGetProperty("placeholder", out var p) ? p.GetString() : "";

        var validation = _validationFactory.Create(json.GetProperty("validation"));

        return new TextField
        {
            Key = key,
            Type = "text",
            Label = label!,
            Placeholder = placeholder!,
            Validation = validation
        };
    }
}