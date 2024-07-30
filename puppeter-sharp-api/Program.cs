using PuppeteerSharp;
using puppeter_sharp_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<RazorTemplateEngine>(); // Register RazorTemplateEngine

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

ResourceExplorer.ListEmbeddedResources();

/*var browserFetcher = new BrowserFetcher();
var installedBrowsers = browserFetcher.GetInstalledBrowsers();

if (!installedBrowsers.Any())
{
    if (browserFetcher.Platform == Platform.Linux)
    {
        browserFetcher.Browser = SupportedBrowser.Chromium;
    }
        // Download Chromium if not already present
        await browserFetcher.DownloadAsync();

    if(browserFetcher.Platform == Platform.Linux)
    {
        var revisionPath = browserFetcher.CacheDir;
        // Assuming that Chromium is extracted into a 'chrome-linux' directory
        var chromiumPath = Path.Combine(revisionPath, "Chromium/Linux-1334075/chrome-linux");

        if (Directory.Exists(chromiumPath))
        {
            var files = Directory.GetFiles(chromiumPath, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // Set permissions to 777 for all files
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"+x {file}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                })?.WaitForExit();
            }
        }
    }
}*/

app.Run();

