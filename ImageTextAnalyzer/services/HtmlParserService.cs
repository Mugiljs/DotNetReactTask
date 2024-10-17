using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

public class HtmlParserService
{
    private readonly HttpClient _httpClient;

    public HtmlParserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetHtmlContentAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public List<string> ExtractImageUrls(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var imageUrls = new List<string>();
        foreach (var img in doc.DocumentNode.SelectNodes("//img[@src]"))
        {
            var src = img.GetAttributeValue("src", null);
            if (!string.IsNullOrEmpty(src))
            {
                imageUrls.Add(src);
            }
        }
        return imageUrls;
    }

    public string ExtractTextContent(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.InnerText;
    }
}