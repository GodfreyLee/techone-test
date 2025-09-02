using NumberToWordsApp.Services;
using Xunit;

namespace NumberToWordsApp.Tests
{
    public class NumberToWordsServiceTests
    {
        private readonly NumberToWordsService _service;

        public NumberToWordsServiceTests()
        {
            _service = new NumberToWordsService();
        }

        [Fact]
        public void ConvertToWords_Zero_ReturnsZeroDollars()
        {
            var result = _service.ConvertToWords("0");
            Assert.Equal("ZERO DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_OneDollar_ReturnsOneDollar()
        {
            var result = _service.ConvertToWords("1");
            Assert.Equal("ONE DOLLAR", result);
        }

        [Fact]
        public void ConvertToWords_MultipleDollars_ReturnsDollarsPlural()
        {
            var result = _service.ConvertToWords("2");
            Assert.Equal("TWO DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_OneCent_ReturnsOneCent()
        {
            var result = _service.ConvertToWords("0.01");
            Assert.Equal("ONE CENT", result);
        }

        [Fact]
        public void ConvertToWords_MultipleCents_ReturnsCentsPlural()
        {
            var result = _service.ConvertToWords("0.99");
            Assert.Equal("NINETY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertToWords_ExampleFromRequirement_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("123.45");
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertToWords_Thousands_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("1000");
            Assert.Equal("ONE THOUSAND DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_ComplexNumber_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("1234567.89");
            Assert.Equal("ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS AND EIGHTY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertToWords_Teens_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("19.19");
            Assert.Equal("NINETEEN DOLLARS AND NINETEEN CENTS", result);
        }

        [Fact]
        public void ConvertToWords_Hundreds_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("500");
            Assert.Equal("FIVE HUNDRED DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_SingleCent_WithoutZeroDollars()
        {
            var result = _service.ConvertToWords("0.01");
            Assert.Equal("ONE CENT", result);
        }

        [Fact]
        public void ConvertToWords_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertToWords(""));
        }

        [Fact]
        public void ConvertToWords_NullInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertToWords(null!));
        }

        [Fact]
        public void ConvertToWords_InvalidNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertToWords("abc"));
        }

        [Fact]
        public void ConvertToWords_NegativeNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertToWords("-123.45"));
        }

        [Fact]
        public void ConvertToWords_NumberTooLarge_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ConvertToWords("1000000000000"));
        }

        [Theory]
        [InlineData("1", "ONE DOLLAR")]
        [InlineData("21", "TWENTY-ONE DOLLARS")]
        [InlineData("101", "ONE HUNDRED AND ONE DOLLARS")]
        [InlineData("1001", "ONE THOUSAND ONE DOLLARS")]
        [InlineData("0.10", "TEN CENTS")]
        [InlineData("10.10", "TEN DOLLARS AND TEN CENTS")]
        public void ConvertToWords_VariousInputs_ReturnsExpectedResults(string input, string expected)
        {
            var result = _service.ConvertToWords(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertToWords_MaximumValue_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("999999999999.99");
            Assert.Equal("NINE HUNDRED AND NINETY-NINE BILLION NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertToWords_OnlyDecimalPart_ReturnsOnlyCents()
        {
            var result = _service.ConvertToWords(".50");
            Assert.Equal("FIFTY CENTS", result);
        }

        [Fact]
        public void ConvertToWords_HundredsWithoutRemainder_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("300");
            Assert.Equal("THREE HUNDRED DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_Eleven_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("11");
            Assert.Equal("ELEVEN DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_TwentyOne_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("21");
            Assert.Equal("TWENTY-ONE DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_OneHundredEleven_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("111");
            Assert.Equal("ONE HUNDRED AND ELEVEN DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_OnlyDollarsNoDecimals_ReturnsCorrectFormat()
        {
            var result = _service.ConvertToWords("50");
            Assert.Equal("FIFTY DOLLARS", result);
        }

        [Fact]
        public void ConvertToWords_DecimalWithZero_ReturnsOnlyDollars()
        {
            var result = _service.ConvertToWords("50.00");
            Assert.Equal("FIFTY DOLLARS", result);
        }
    }
}