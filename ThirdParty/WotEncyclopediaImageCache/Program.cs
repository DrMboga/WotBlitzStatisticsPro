using System.Net.Http.Headers;
using WotEncyclopediaImageCache;
using System.Text.Json;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", "WotBlitzStatisticsPro");

var vehicles = await GetVehicles(client);

var dirName = "vehicles";
var width = 95;
var height = 70;

if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, dirName)))
{
    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, dirName));
    Console.WriteLine(Path.Combine(Environment.CurrentDirectory, dirName));
}

foreach(var vehicle in vehicles)
{
    var imageUrl = vehicle.Value.Images["preview"];
    if(imageUrl != null)
    {
        var url = new Uri(vehicle.Value.Images["preview"]);
        // Console.WriteLine(url.AbsoluteUri);
        var fileName = url.Segments[url.Segments.Length - 1];
        var imageBuff = await(DownloadImage(imageUrl));
        using (Image image = Image.Load<Rgba32>(imageBuff))
        {
            image.Mutate(x => x.Resize(width, height));

            image.Save(Path.Combine(Environment.CurrentDirectory, dirName, fileName));
            Console.WriteLine(Path.Combine(Environment.CurrentDirectory, dirName, fileName));
        }
    }
}

static async Task<Dictionary<string, EncyclopediaEntry>> GetVehicles(HttpClient client)
{
    var url = "https://api.wotblitz.eu/wotb/encyclopedia/vehicles/?application_id=7026c24084fc704a9b73d8b35c5ff45a&fields=images";
    var vehiclesSerialized = await client.GetStringAsync(url);
    // await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var vehicles = JsonSerializer.Deserialize<ResponseBody<Dictionary<string, EncyclopediaEntry>>>(vehiclesSerialized);
    return vehicles.Data;
}

static async Task<byte[]?> DownloadImage(string url)
{
    using (var client = new HttpClient())  
    using (var result = await client.GetAsync(url))  
    return result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync() : null;
}

