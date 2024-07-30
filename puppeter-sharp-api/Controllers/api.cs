using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using puppeter_sharp_api.Models;
using puppeter_sharp_api.Services;

namespace puppeter_sharp_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly RazorTemplateEngine _templateEngine;

        public TemplatesController(RazorTemplateEngine templateEngine)
        {
            _templateEngine = templateEngine;
        }

        [HttpGet("render")]
        public async Task<IActionResult> RenderTemplate()
        {
            // Define mock data
            var mockModel = new MockModel
            {
                Title = "Sample Report",
                Content = "This is a sample content for the report."
            };

            // Define the template name (must match the embedded resource name)
            string templateName = "ReportTemplate.cshtml";

            // Render the template with mock data
            var renderedHtml = await _templateEngine.RenderTemplateAsync(templateName, mockModel);

            var pdfStream = await GeneratePdfAsync(renderedHtml);

            // Return PDF file
            return File(pdfStream, "application/pdf", "Report.pdf");
        }

        private async Task<Stream> GeneratePdfAsync(string htmlContent)
        {
            /*var browserFetcher = new BrowserFetcher();
            browserFetcher.Browser = SupportedBrowser.Chromium;
            var executablePath = browserFetcher.GetExecutablePath("1334075");*/

            // Launch the browser and create a page
            await using var browser = await Puppeteer.LaunchAsync(
                    new LaunchOptions { 
                        //ExecutablePath = executablePath,
                        Headless = true,
                        //Browser = SupportedBrowser.Chromium,
                        Args = new[] {
                          "--disable-gpu",
                          "--disable-dev-shm-usage",
                          "--disable-setuid-sandbox",
                          "--no-sandbox"}
                        });
            await using var page = await browser.NewPageAsync();

            // Set content and generate PDF
            await page.SetContentAsync(htmlContent);
            var pdfStream = new MemoryStream(await page.PdfDataAsync());

            return pdfStream;
        }
    }
}
