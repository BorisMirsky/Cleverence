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
        public void Encode_WithExampleFromTask_ReturnsCorrectString()
        {
            // Arrange
            EncodeDecode encDec = new();
            // Act
            string[] result = encDec.Handle("aaabbcccdde");
            // Assert
            Assert.Equal(["a3b2c3d2e", "aaabbcccdde"], result);
        }

        [Fact]
        public void Encode_WithExampleFromTask_ReturnsCorrectString_1()
        {
            // Arrange
            EncodeDecode encDec = new();
            // Act
            string[] result = encDec.Handle("qabbbccdddee");
            // Assert
            Assert.Equal(["qa3b2c3d2e", "qabbbccdddee"], result);
        }

        [Fact]
        public void Encode_WithAllSameChars_ReturnsCharWithCount()
        {
            var encDec = new EncodeDecode();
            var result = encDec.Handle("aaaa");
            Assert.Equal(["a4", "aaaa"], result);
        }

        [Fact]
        public void Encode_WithNoRepeats_ReturnsSameString()
        {
            var encDec = new EncodeDecode();
            var result = encDec.Handle("abcde");
            Assert.Equal(["abcde", "abcde"], result);
        }

        [Fact]
        public void Encode_WithRepeats_ReturnsCompressed()
        {
            var encDec = new EncodeDecode();
            string result = encDec.Encode("aaabb");
            Assert.Equal("a3b2", result);
        }

        [Fact]
        public void Decode_WithCompressed_ReturnsOriginal()
        {
            var encDec = new EncodeDecode();
            string result = encDec.Decode("a3b2");
            Assert.Equal("aaabb", result);
        }

        [Fact]
        public void Decode_WithMultiDigitNumbers_WorksCorrectly()
        {
            var encDec = new EncodeDecode();
            string result = encDec.Decode("a12b3");
            Assert.Equal("aaaaaaaaaaaabbb", result);
        }


    }
}
