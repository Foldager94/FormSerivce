using System.Text.Json;
using Domain.Interfaces;

namespace Application.Factories;

public interface IFieldValidationFactory
{
    IFieldValidation Create(JsonElement json);
}