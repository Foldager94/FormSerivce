using System.Text.Json;
using System.Text.RegularExpressions;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure;

public class DataValidation
{
    public List<string> ValidateAgainstSchema(JsonElement data, IFormSchema schema)
    {
        var errors = new List<string>();

        foreach (var field in schema.Fields)
        {
            if (field.Key == "id") continue;

            if (!data.TryGetProperty(field.Key, out var jsonProp))
            {
                if (field.Validation.Required)
                    errors.Add($"Missing required field: {field.Key}");
                continue;
            }

            var value = jsonProp.GetString();

            if (field.Validation.Required && string.IsNullOrWhiteSpace(value))
                errors.Add($"Field '{field.Key}' is required.");

            if (field.Validation.MinLength.HasValue && value?.Length < field.Validation.MinLength)
                errors.Add($"Field '{field.Key}' is too short. Min length is {field.Validation.MinLength}");

            if (field.Validation.MaxLength.HasValue && value?.Length > field.Validation.MaxLength)
                errors.Add($"Field '{field.Key}' is too long. Max length is {field.Validation.MaxLength}");

            if (!string.IsNullOrWhiteSpace(field.Validation.Pattern) &&
                value is not null &&
                !Regex.IsMatch(value, field.Validation.Pattern))
                errors.Add($"Field '{field.Key}' does not match required pattern.");
        }

        return errors;
    }
}