using System.Text.Json;
using Domain.Interfaces;

namespace Application;

public interface IFormSchemaParser
{
    IFormSchema Parse(JsonDocument jsonDoc);
}