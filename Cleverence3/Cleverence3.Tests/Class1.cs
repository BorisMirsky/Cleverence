using Xunit;


namespace Cleverence3.Tests
{
    public class Class1
    {
        [Fact]
        public void CheckDate()
        {
            // Arrange
            PreProcessString proc = new();
            // Act
            string result = proc.ProcessDate("20d5-i1-22");
            // Assert
            Assert.Equal("NOVALID", result);
        }


        [Fact]
        public void CheckLoginLevel()
        {
            // Arrange
            PreProcessString proc = new();
            // Act
            string result = proc.LoggingLevel("IN3FO");
            // Assert
            Assert.Equal("NOVALID", result);
        }
    }
}
