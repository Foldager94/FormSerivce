using System.Text.Json;
using Infrastructure.Extensions;

namespace Test.Infrastructure.Extensions
{
    [TestFixture]
    public class JsonElementExtensionTests
    {
        private JsonElement Parse(string json) => JsonDocument.Parse(json).RootElement;
        

        [Test]
        public void GetRequiredString_ValidProperty_ReturnsValue()
        {
            var json = Parse(@"{ ""name"": ""TestValue"" }");
            var result = json.GetRequiredString("name");
            Assert.That(result, Is.EqualTo("TestValue"));
        }

        [Test]
        public void GetRequiredString_MissingProperty_ThrowsKeyNotFoundException()
        {
            var json = Parse("{ }");
            Assert.Throws<KeyNotFoundException>(() => json.GetRequiredString("missing"));
        }

        [Test]
        public void GetRequiredString_NonStringValue_ThrowsFormatException()
        {
            var json = Parse(@"{ ""age"": 123 }");
            Assert.Throws<FormatException>(() => json.GetRequiredString("age"));
        }

        [Test]
        public void GetRequiredString_EmptyOrWhitespace_ThrowsArgumentNullException()
        {
            var jsonEmpty = Parse(@"{ ""tag"": """" }");
            var jsonWhite = Parse(@"{ ""tag"": ""   "" }");

            Assert.Throws<ArgumentNullException>(() => jsonEmpty.GetRequiredString("tag"));
            Assert.Throws<ArgumentNullException>(() => jsonWhite.GetRequiredString("tag"));
        }

        [Test]
        public void GetRequiredGuid_ValidGuid_ReturnsGuid()
        {
            var id = Guid.NewGuid();
            var json = Parse($@"{{ ""id"": ""{id}"" }}");
            var result = json.GetRequiredGuid("id");
            Assert.That(result, Is.EqualTo(id));
        }

        [Test]
        public void GetRequiredGuid_MissingProperty_ThrowsKeyNotFoundException()
        {
            var json = Parse("{ }");
            Assert.Throws<KeyNotFoundException>(() => json.GetRequiredGuid("id"));
        }

        [Test]
        public void GetRequiredGuid_InvalidGuidFormat_ThrowsFormatException()
        {
            var json = Parse(@"{ ""id"": ""not-a-guid"" }");
            Assert.Throws<FormatException>(() => json.GetRequiredGuid("id"));
        }

        [Test]
        public void GetRequiredGuid_EmptyGuid_ThrowsArgumentNullException()
        {
            var emptyValue = "";
            var json = Parse($@"{{ ""id"": ""{emptyValue}"" }}");
            Assert.Throws<ArgumentNullException>(() => json.GetRequiredGuid("id"));
        }
        
    }
}
