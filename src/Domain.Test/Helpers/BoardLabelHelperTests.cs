using MinefieldBoard.Domain.Helpers;
using Shouldly;

namespace MinefieldBoard.Domain.Test.Unit.Helpers
{
    [TestClass]
    public class BoardLabelHelperTests
    {
        [TestMethod]
        public void GetXLabel_Value_Test()
        {
            // Arrange 

            // Act
            var value = BoardLabelHelper.GetXLabel(5);

            // Assert
            value.ShouldBe("F");
        }
    }
}
