namespace NumberToWordsApp.Models
{
    public class ConversionResponse
    {
        public string Words { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}