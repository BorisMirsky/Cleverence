using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Cleverence1;



namespace Cleverence1.Tests
{
    public class EncodeDecodeStringTests
    {
        [Theory]
        [InlineData("qqqwwe")]
        public void EncodeReturnNotNull(string input)
        {
           
            // Arrange
            EncodeDecode encDec = new();

            //string input = "qaaabbcccdde";

            // Act
            string[] result = encDec.Handle("qaaabbcccdde");

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);


            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            else if (input.Length == 1)
            {
                return input;
            }

            StringBuilder encoded = new StringBuilder();
            char temp = input[0];
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == temp)
                {
                    count++;
                }
                else
                {
                    if (count == 1)
                    {
                        encoded.Append(temp);
                        temp = input[i];
                    }
                    else
                    {
                        encoded.Append(count);
                        encoded.Append(temp);
                        temp = input[i];
                        count = 1;
                    }
                }
            }

            if (count != 1)
            {
                encoded.Append(count);
            }
            encoded.Append(temp);

            return encoded.ToString();
        }
    }
}
