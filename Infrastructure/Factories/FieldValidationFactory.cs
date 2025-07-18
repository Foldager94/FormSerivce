using System.Text.Json;
using Application.Factories;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Factories;

public class FieldValidationFactory : IFieldValidationFactory
{
    public IFieldValidation Create(JsonElement json)
    {
        var required = json.TryGetProperty("required", out var req) && req.GetBoolean();
        int? minLength = json.TryGetProperty("minLength", out var min)
            ? min.GetInt32()
            : null;
        int? maxLength = json.TryGetProperty("maxLength", out var max)
            ? max.GetInt32()
            : null;
        var pattern = json.TryGetProperty("pattern", out var pat)
            ? pat.GetString()
            : null;

        return new FieldValidation()
        {
            Required = required,
            MinLength = minLength,
            MaxLength = maxLength,
            Pattern = pattern
        };
    }
}