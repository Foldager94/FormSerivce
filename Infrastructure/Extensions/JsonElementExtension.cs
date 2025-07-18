using System.Text.Json;

namespace Infrastructure.Extensions;

public static class JsonElementExtension
{
    public static Guid GetRequiredGuid(this JsonElement json, string propertyName)
    {
        var jsonString = GetRequiredString(json, propertyName);
        if (!Guid.TryParse(jsonString, out var guid))
            throw new FormatException($"Invalid GUID format for '{propertyName}': '{jsonString}'");

        return guid;
    }

    public static string GetRequiredString(this JsonElement json, string propertyName)
    {
        if (!json.TryGetProperty(propertyName, out var elem))
            throw new KeyNotFoundException($"Missing '{propertyName}' property in JSON");

        if (elem.ValueKind != JsonValueKind.String)
            throw new FormatException($"Expected '{propertyName}' to be a JSON string");

        var value = elem.GetString();
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(propertyName, $"'{propertyName}' cannot be null or empty");

        return value;
    }
}