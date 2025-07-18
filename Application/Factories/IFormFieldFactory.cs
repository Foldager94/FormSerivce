using System.Text.Json;
using Domain.Interfaces;

namespace Application.Factories;

public interface IFormFieldFactory
{
    IFormField Create(JsonElement json);
}