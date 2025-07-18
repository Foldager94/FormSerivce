using System.Text.Json;
using Domain.Interfaces;

namespace Application.Factories;

public interface IFormSchemaFactory
{
    IFormSchema Create(JsonElement json);
}