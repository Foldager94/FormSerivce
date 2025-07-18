using System;
using System.Collections.Generic;
using System.Text.Json;
using Application.Factories;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Extensions;

namespace Infrastructure.Factories
{
    public class FormSchemaFactory : IFormSchemaFactory
    {
        public IFormSchema Create(JsonElement json)
        {
            var id = json.GetRequiredGuid("id");
            var title = json.GetRequiredString("title");
            var version = json.GetRequiredString("version");

            return new FormSchema
            {
                Id = id,
                Title = title,
                Version = version
            };
        }
        
    }
}