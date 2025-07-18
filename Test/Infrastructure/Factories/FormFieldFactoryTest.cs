using System;
using System.Text.Json;
using Application.Factories;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Factories;
using NUnit.Framework;

namespace Test.Infrastructure.Factories
{
    [TestFixture]
    public class FormFieldFactoryTests
    {
        private class StubValidationFactory : IFieldValidationFactory
        {
            public IFieldValidation Create(JsonElement json)
                => new FieldValidation
                {
                    Required = true,
                    MinLength = 1,
                    MaxLength = 2,
                    Pattern = "X"
                };
        }

        private FormFieldFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new FormFieldFactory(new StubValidationFactory());
        }

        [Test]
        public void Create_WithAllProperties_ShouldMapCorrectly()
        {
            var json = JsonDocument.Parse(@"
            {
                ""key"": ""username"",
                ""type"": ""text"",
                ""label"": ""User Name"",
                ""placeholder"": ""Enter your name"",
                ""validation"": {}
            }").RootElement;

            var result = _factory.Create(json);

            Assert.That(result.Key, Is.EqualTo("username"));
            Assert.That(result.Type, Is.EqualTo("text"));
            Assert.That(result.Label, Is.EqualTo("User Name"));
            Assert.That(result.Placeholder, Is.EqualTo("Enter your name"));
            Assert.That(result.Validation.Required, Is.True);
        }

        [Test]
        public void Create_MissingOptional_ShouldDefaultToEmptyString()
        {
            var json = JsonDocument.Parse(@"
            {
                ""key"": ""email"",
                ""type"": ""text"",
                ""validation"": {}
            }").RootElement;

            var result = _factory.Create(json);

            Assert.That(result.Label, Is.EqualTo(""));
            Assert.That(result.Placeholder, Is.EqualTo(""));
        }
    }
}
