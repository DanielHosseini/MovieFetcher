using Newtonsoft.Json;
using PCLStorage;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieFetcher.Core
{
    public class JSONHandler
    {
        private static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Gets the JSON data from the rest API
        /// Parses the JSON to object and returns
        /// </summary>
        public async Task<YIFYMovies> ParseJsonAsync(string url)
        {
			var jsonResponse = await httpClient.GetStringAsync(url);
            var YIFYMovies = JsonConvert.DeserializeObject<YIFYMovies>(jsonResponse);

            return YIFYMovies;
        }

        /// <summary>
        /// Todo, save JSON data to file and use it for offline mode
        /// </summary>
        private async Task WriteJSONToLocalFileAsync(string JSONContent)
        {
            string fileName = "JSONMovieData.txt";
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("storage", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JSONContent);
        }

        /// <summary>
        /// Todo, read JSON data from file
        /// </summary>
        public async Task<string> ReadJSONFromFileAsync()
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync("JSONMovieData.txt");
            var jsonContent = await file.ReadAllTextAsync();

            return jsonContent;
        }
    }
}