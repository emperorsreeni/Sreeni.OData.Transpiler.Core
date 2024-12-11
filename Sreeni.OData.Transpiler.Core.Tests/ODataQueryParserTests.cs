using Sreeni.OData.Transpiler.Core;
using Shouldly;

namespace Sreeni.OData.Transpiler.Tests
{
    [TestClass]
    public class ODataQueryParserTests
    {
        private ODataQueryParser sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new ODataQueryParser();
        }

        [TestMethod]
        public void Parse_ShouldParseSelectQuery()
        {
            // Arrange
            var query = "$select=Name,Age";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.Select.ShouldBe(new List<string> { "Name", "Age" });
        }

        [TestMethod]
        public void Parse_ShouldParseFilterQuery()
        {
            // Arrange
            var query = "$filter=Name eq 'John'";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.Filter.ShouldBe("Name eq 'John'");
        }

        [TestMethod]
        public void Parse_ShouldParseOrderByQuery()
        {
            // Arrange
            var query = "$orderby=Name,Age desc";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.OrderBy.ShouldBe(new List<string> { "Name", "Age desc" });
        }

        [TestMethod]
        public void Parse_ShouldParseTopQuery()
        {
            // Arrange
            var query = "$top=10";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.Top.ShouldBe(10);
        }

        [TestMethod]
        public void Parse_ShouldParseSkipQuery()
        {
            // Arrange
            var query = "$skip=5";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.Skip.ShouldBe(5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_ShouldThrowArgumentExceptionForInvalidQueryPart()
        {
            // Arrange
            var query = "invalidQueryPart";

            // Act
            sut.Parse(query);
        }

        [TestMethod]
        public void Parse_ShouldHandleMultipleQueryParts()
        {
            // Arrange
            var query = "$select=Name,Age&$filter=Name eq 'John'&$orderby=Name&$top=10&$skip=5";

            // Act
            var result = sut.Parse(query);

            // Assert
            result.Select.ShouldBe(new List<string> { "Name", "Age" });
            result.Filter.ShouldBe("Name eq 'John'");
            result.OrderBy.ShouldBe(new List<string> { "Name" });
            result.Top.ShouldBe(10);
            result.Skip.ShouldBe(5);
        }

        
        [TestMethod]
        public void Parse_ShouldParseAndOrGroupFilterQuery()
        {
            // Arrange
            var query = "$filter=(Name eq 'John' or Age eq 30) and (City eq 'New York' or State eq 'NY')";
            // Act
            var result = sut.Parse(query);
            // Assert
            result.Filter.ShouldBe("(Name eq 'John' or Age eq 30) and (City eq 'New York' or State eq 'NY')");
        }
        
    }
}
