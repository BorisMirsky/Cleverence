using Xunit;


namespace Cleverence2.Tests
{
    public class ReaderWriterTests
    {
        [Fact]
        public void ReturnEmptyString()
        {
            // Arrange
            ReaderWriter rwLock = new();
            // Act
            rwLock.AddToCount(100);
            int result = rwLock.count;
            int result1 = rwLock.GetCount();
            // Assert
            Assert.Equal(result, result1);
        }
    }
}
