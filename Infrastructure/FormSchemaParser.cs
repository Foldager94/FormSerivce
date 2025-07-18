using System.Text.Json;
using Application;
using Application.Factories;
using Domain.Interfaces;

namespace Infrastructure;

public class FormSchemaParser(IFormSchemaFactory schemaFactory, IFormFieldFactory fieldFactory) : IFormSchemaParser
{
    public IFormSchema Parse(JsonDocument jsonDoc)
    {
        var root = jsonDoc.RootElement;
        var formSchema = schemaFactory.Create(root);
        foreach (var field in root.GetProperty("fields").EnumerateArray()){
            formSchema.Fields.Add(fieldFactory.Create(field));
        }
        
        return formSchema;
        
    }
}