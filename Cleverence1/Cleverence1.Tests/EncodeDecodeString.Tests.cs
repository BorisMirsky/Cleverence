using Xunit;


namespace Cleverence1.Tests
{
    public class EncodeDecodeTests
    {
        [Fact]
        public void ReturnEmptyString()
        {        
            // Arrange
            EncodeDecode encDec = new();
            // Act
            string[] result = encDec.Handle("");
            // Assert
            Assert.Equal(["", ""], result);
        }

        [Fact]
        public void ReturnOneChar()
        {
            // Arrange
            EncodeDecode encDec = new();
            // Act
            string[] result = encDec.Handle("q");
            // Assert
            Assert.Equal(["q", "q"], result);
        }

        [Fact]
        public void ReturnString()
        {
            // Arrange
            EncodeDecode encDec = new();
            // Act
            string[] result = encDec.Handle("qaaabbcccdde");
            // Assert
            Assert.Equal(["q3a2b3c2de", "qaaabbcccdde"], result);
        }

        [Fact]
        public void DetectDigit()
        {
            // Arrange
            EncodeDecode encDec = new();
            // Act
            bool result = encDec.StringIsDigits("qaaa5bbcccdde");
            // Assert
            Assert.False(result);
        }
    }
}
