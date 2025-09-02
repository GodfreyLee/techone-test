using Microsoft.AspNetCore.Mvc;
using NumberToWordsApp.Models;
using NumberToWordsApp.Services;

namespace NumberToWordsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberToWordsController : ControllerBase
    {
        private readonly NumberToWordsService _numberToWordsService;

        public NumberToWordsController(NumberToWordsService numberToWordsService)
        {
            _numberToWordsService = numberToWordsService;
        }

        [HttpPost("convert")]
        public IActionResult ConvertToWords([FromBody] ConversionRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Number))
                {
                    return BadRequest(new ConversionResponse 
                    { 
                        Success = false, 
                        ErrorMessage = "Number is required" 
                    });
                }

                var words = _numberToWordsService.ConvertToWords(request.Number);
                
                return Ok(new ConversionResponse 
                { 
                    Words = words, 
                    Success = true 
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ConversionResponse 
                { 
                    Success = false, 
                    ErrorMessage = ex.Message 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ConversionResponse 
                { 
                    Success = false, 
                    ErrorMessage = "An unexpected error occurred: " + ex.Message 
                });
            }
        }
    }
}