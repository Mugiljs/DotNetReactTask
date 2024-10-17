using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/analyze/")]
public class AnalyzeController : ControllerBase
{
    private readonly HtmlParserService _parserService;

    public AnalyzeController(HtmlParserService parserService)
    {
        _parserService = parserService;
    }

    [HttpPost("extract")]
    public async Task<IActionResult> ExtractData([FromBody] UrlRequest request)
    {
        var htmlContent = await _parserService.GetHtmlContentAsync(request.Url);
        var imageUrls = _parserService.ExtractImageUrls(htmlContent);
        var textContent = _parserService.ExtractTextContent(htmlContent);

        
        var wordCount = textContent.Split(' ')
            .Where(word => !string.IsNullOrEmpty(word))
            .GroupBy(word => word.ToLower())
            .ToDictionary(g => g.Key, g => g.Count());

        return Ok(new { Images = imageUrls, WordCount = wordCount });
    }
}

public class UrlRequest
{
    public string Url { get; set; }
}