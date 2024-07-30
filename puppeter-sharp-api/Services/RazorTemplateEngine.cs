using RazorLight;
using System.Reflection;

namespace puppeter_sharp_api.Services
{
    public class RazorTemplateEngine
    {
        private readonly RazorLightEngine _engine;

        public RazorTemplateEngine()
        {
            _engine = new RazorLightEngineBuilder()
                            //.UseEmbeddedResourcesProject(typeof(RazorTemplateEngine))
                            .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates"))

                .UseMemoryCachingProvider()
                .Build();
            _engine.Options.EnableDebugMode = true;
        }

        public async Task<string> RenderTemplateAsync<TModel>(string templateName, TModel model)
        {
            var assembly = Assembly.GetExecutingAssembly();
            /*var resourceName = $"puppeter_sharp_api.Templates.{templateName}"; // Adjust based on namespace and folder

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new FileNotFoundException($"Template resource '{resourceName}' not found.");
            }

            using var reader = new StreamReader(stream);
            var template = await reader.ReadToEndAsync();*/

            return await _engine.CompileRenderAsync(templateName, model);
        }
    }
}
