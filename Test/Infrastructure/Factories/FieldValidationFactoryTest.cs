using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Factories;
using NUnit.Framework;

namespace Test.Infrastructure.Factories
{
    [TestFixture]
    public class FieldValidationFactoryTests
    {
        private FieldValidationFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new FieldValidationFactory();
        }

        [Test]
        public void Create_AllPropertiesPresent_ShouldMapCorrectly()
        {
            var json = JsonDocument.Parse(@"
            {
                ""required"": true,
                ""minLength"": 5,
                ""maxLength"": 10,
                ""pattern"": ""^[A-Z]+$""
            }").RootElement;

            var result = _factory.Create(json);

            Assert.That(result.Required, Is.True);
            Assert.That(result.MinLength, Is.EqualTo(5));
            Assert.That(result.MaxLength, Is.EqualTo(10));
            Assert.That(result.Pattern, Is.EqualTo("^[A-Z]+$"));
        }

        [Test]
        public void Create_Empty_ShouldDefault()
        {
            var json = JsonDocument.Parse("{}").RootElement;

            var result = _factory.Create(json);

            Assert.That(result.Required, Is.False);
            Assert.That(result.MinLength, Is.Null);
            Assert.That(result.MaxLength, Is.Null);
            Assert.That(result.Pattern, Is.Null);
        }
    }
}