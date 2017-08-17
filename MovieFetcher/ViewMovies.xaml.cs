using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieFetcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMovies : ContentPage
    {
        private string _yifyUrl = "https://yts.ag/api/v2/list_movies.json?limit=10&sort_by=year&order_by=desc";

        public ViewMovies()
        {
            InitializeComponent();
        }

        private async void FetchMovies(object sender, System.EventArgs e)
        {
            loadingIndictor.IsRunning = true;
            try
            {
                var YIFYMoviesResponse = await ParseJsonAsync(_yifyUrl);
            }
            catch (Exception)
            {
                //Throw error to UI
                throw;
            }
            loadingIndictor.IsRunning = false;

            /*  foreach (var movie in root.Data.Movies)
              {
                  Debug.WriteLine(movie.Title);
                  jsonLabel.Text += movie.Title;
              }*/
        }

        public async Task<YIFYMovies> ParseJsonAsync(string url)
        {
            HttpClient htClient = new HttpClient();
            var response = await htClient.GetStringAsync(url);
            var YIFYMovies = JsonConvert.DeserializeObject<YIFYMovies>(response);

            return YIFYMovies;
        }
    }
}