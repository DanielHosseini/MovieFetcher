using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieFetcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMovies : ContentPage
    {
        private const string YIFYURL = "https://yts.ag/api/v2/list_movies.json?limit=20&sort_by=year&order_by=desc";
        private int movieNumber = 0;

        public ViewMovies()
        {
            InitializeComponent();
            FetchMovies();
        }

        private async void FetchMovies()
        {
            loadingIndictor.IsRunning = true;
            try
            {
                var YIFYMoviesResponse = await ParseJsonAsync(YIFYURL);
                PopulateUiGridView(YIFYMoviesResponse);
            }
            catch (Exception)
            {
                //Throw error to UI
                throw;
            }

		}

        public void PopulateUiGridView(YIFYMovies yifyMovies)
        {
			loadingIndictor.IsRunning = false;
			var TotalAmountOfMovies = yifyMovies.data.limit;
            IList<Movy> movieObject = yifyMovies.data.movies;
            var scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
            };
            var grid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0
                   
            };

            for (int row = 0; row < TotalAmountOfMovies / 2; row++)
			{



				for (int column = 0; column < 2; column++)
                {

                    grid.Children.Add(new Image {Aspect = Aspect.Fill, Source = new Uri(movieObject[movieNumber].large_cover_image) }, column, row);



					movieNumber++;
                }
			

			}



			Content = scroll;
            scroll.Content = grid;
        }

        private async Task<YIFYMovies> ParseJsonAsync(string url)
        {
            HttpClient htClient = new HttpClient();
            var jsonResponse = await htClient.GetStringAsync(url);
           // await WriteJSONToLocalFileAsync(jsonResponse);
            var YIFYMovies = JsonConvert.DeserializeObject<YIFYMovies>(jsonResponse);

            return YIFYMovies;
        }

        private async Task WriteJSONToLocalFileAsync(string JSONContent)
        {
            string fileName = "JSONMovieData.txt";
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("storage", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JSONContent);
            await DisplayAlert("Operation Done", "JSON to File", "OK");

        }

        public async Task<string> ReadJSONFromFileAsync()
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync("JSONMovieData.txt");
            var jsonContent = await file.ReadAllTextAsync();
            await DisplayAlert("Operation Done", "Reading JSON from file", "OK");

            return jsonContent;
        }

        private void Fetch_Movies(object sender, EventArgs e)
        {

        }
    }
}
