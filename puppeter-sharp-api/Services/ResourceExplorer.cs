using System.Reflection;

namespace puppeter_sharp_api.Services
{
    public class ResourceExplorer
    {
        public static void ListEmbeddedResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();

            Console.WriteLine("Embedded Resources:");
            foreach (var name in resourceNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}
