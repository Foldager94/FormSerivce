using System;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Factories;
using NUnit.Framework;

namespace Test.Infrastructure.Factories
{
    [TestFixture]
    public class FormSchemaFactoryTests
    {
        private FormSchemaFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new FormSchemaFactory();
        }

        [Test]
        public void Create_WithAllProperties_ShouldMapCorrectly()
        {
            var id = Guid.NewGuid();
            var json = JsonDocument.Parse($@"
            {{
                ""id"": ""{id}"",
                ""title"": ""My Form"",
                ""version"": ""v1""
            }}").RootElement;

            var result = _factory.Create(json);

            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Title, Is.EqualTo("My Form"));
            Assert.That(result.Version, Is.EqualTo("v1"));
        }
    }
}