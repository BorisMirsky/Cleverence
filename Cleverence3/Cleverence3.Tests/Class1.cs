using Xunit;
using System.IO;

namespace Cleverence3.Tests
{
    public class PreProcessStringTests
    {
        private readonly PreProcessString _processor;
        private readonly string _testResultsPath;
        private readonly string _testProblemsPath;

        public PreProcessStringTests()
        {
            _processor = new PreProcessString();
            _testResultsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt");
            _testProblemsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "problems.txt");

            // Очищаем тестовые файлы перед каждым запуском
            if (File.Exists(_testResultsPath)) File.Delete(_testResultsPath);
            if (File.Exists(_testProblemsPath)) File.Delete(_testProblemsPath);
        }


        [Fact]
        public void Parse_Format1_ValidInput_ReturnsCorrectParsedLog()
        {
            // Arrange
            string input = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

            // Act
            var result = _processor.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("10.03.2025", result.Date);
            Assert.Equal("15:14:49.523", result.Time);
            Assert.Equal("INFO", result.Level);
            Assert.Equal("DEFAULT", result.Method);
            Assert.Equal("Версия программы: '3.4.0.48729'", result.Message);
        }

        [Fact]
        public void Parse_Format2_ValidInput_ReturnsCorrectParsedLog()
        {
            // Arrange
            string input = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

            // Act
            var result = _processor.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("2025-03-10", result.Date);
            Assert.Equal("15:14:51.5882", result.Time);
            Assert.Equal("INFO", result.Level);
            Assert.Equal("MobileComputer.GetDeviceId", result.Method);
            Assert.Equal("Код устройства: '@MINDEO-M40-D-410244015546'", result.Message);
        }

        [Fact]
        public void Parse_Format2_WarningLevel_ConvertsToWARN()
        {
            // Arrange
            string input = "2025-03-10 15:14:51.5882| WARNING|11|SomeMethod| Тестовое сообщение";

            // Act
            var result = _processor.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("WARN", result.Level);
        }

        [Fact]
        public void Parse_Format1_WarningLevel_ConvertsToWARN()
        {
            // Arrange
            string input = "10.03.2025 15:14:49.523 WARNING Тестовое сообщение";

            // Act
            var result = _processor.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("WARN", result.Level);
        }

        [Fact]
        public void Parse_Format2_ErrorLevel_KeepsERROR()
        {
            // Arrange
            string input = "2025-03-10 15:14:51.5882| ERROR|11|SomeMethod| Ошибка";

            // Act
            var result = _processor.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ERROR", result.Level);
        }

        [Fact]
        public void Handle_Format1_ValidInput_WritesToResultFile()
        {
            // Arrange
            string input = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Contains("10-03-2025", result);
            Assert.Contains("INFO", result);
            Assert.Contains("DEFAULT", result);
            Assert.Contains("Версия программы:", result);
            Assert.True(File.Exists(_testResultsPath));
        }

        [Fact]
        public void Handle_Format1_ValidInput_DateFormatConvertedToDDMMYYYY()
        {
            // Arrange
            string input = "10.03.2025 15:14:49.523 INFORMATION Тест";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.StartsWith("10-03-2025", result);
        }

        [Fact]
        public void Handle_Format2_ValidInput_DateFormatConvertedToDDMMYYYY()
        {
            // Arrange
            string input = "2025-03-10 15:14:51.5882| INFO|11|Method| Тест";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.StartsWith("10-03-2025", result);
        }

        [Fact]
        public void Handle_ValidInput_FieldsSeparatedByTab()
        {
            // Arrange
            string input = "10.03.2025 15:14:49.523 INFORMATION Тест";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Contains("\t", result);
            string[] parts = result.Split('\t');
            Assert.Equal(5, parts.Length); 
        }


        [Fact]
        public void Handle_InvalidInput_NoDate_WritesToProblemsFile()
        {
            // Arrange
            string input = "invalid log without date";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Equal("INVALID", result);
            Assert.True(File.Exists(_testProblemsPath));

            string problemsContent = File.ReadAllText(_testProblemsPath);
            Assert.Contains(input, problemsContent);
        }

        [Fact]
        public void Handle_InvalidInput_NoTime_WritesToProblemsFile()
        {
            // Arrange
            string input = "2025-03-10 INFO no time here";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Equal("INVALID", result);
            Assert.True(File.Exists(_testProblemsPath));
        }

        [Fact]
        public void Handle_InvalidInput_NoLevel_WritesToProblemsFile()
        {
            // Arrange
            string input = "2025-03-10 15:14:49.523 no level here";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Equal("INVALID", result);
            Assert.True(File.Exists(_testProblemsPath));
        }

        [Fact]
        public void Handle_InvalidInput_WritesOriginalLineNotProcessed()
        {
            // Arrange
            string input = "gibberish 12345 !!!";

            // Act
            _processor.Handle(input);

            // Assert
            string problemsContent = File.ReadAllText(_testProblemsPath);
            Assert.Equal(input, problemsContent.Trim()); 
        }

        [Fact]
        public void Handle_MultipleInvalidInputs_AppendsToProblemsFile()
        {
            // Arrange
            string input1 = "invalid line 1";
            string input2 = "invalid line 2";

            // Act
            _processor.Handle(input1);
            _processor.Handle(input2);

            // Assert
            string[] lines = File.ReadAllLines(_testProblemsPath);
            Assert.Equal(2, lines.Length);
            Assert.Contains(input1, lines);
            Assert.Contains(input2, lines);
        }


        [Fact]
        public void Handle_EmptyString_WritesToProblemsFile()
        {
            // Arrange
            string input = "";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Equal("INVALID", result);
            Assert.True(File.Exists(_testProblemsPath));
        }

        [Fact]
        public void Handle_NullInput_WritesToProblemsFile()
        {
            // Arrange
            string? input = null;

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Equal("INVALID", result);
            Assert.True(File.Exists(_testProblemsPath));
        }

        [Fact]
        public void Handle_Format2_WithoutMethod_AddsDefault()
        {
            // Arrange
            string input = "2025-03-10 15:14:51.5882| INFO|11|";

            // Act
            var result = _processor.Handle(input);

            // Assert
            Assert.Contains("DEFAULT", result);
        }
    }
}