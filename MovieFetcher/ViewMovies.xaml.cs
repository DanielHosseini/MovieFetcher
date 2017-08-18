using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            fetchBtn.IsEnabled = false;
            try
            {
                var YIFYMoviesResponse = await ParseJsonAsync(_yifyUrl);
                PopulateUiGridView(YIFYMoviesResponse);
            }
            catch (Exception)
            {
                //Throw error to UI
                throw;
            }
            loadingIndictor.IsRunning = false;
            fetchBtn.IsEnabled = true;

            /*  foreach (var movie in root.Data.Movies)
              {
                  Debug.WriteLine(movie.Title);
                  jsonLabel.Text += movie.Title;
              }*/
        }

        public void PopulateUiGridView(YIFYMovies yifyMovies)
        {
            var TotalAmountOfMovies = yifyMovies.data.limit;
            IList<Movy> movieNames = yifyMovies.data.movies;
            Debug.WriteLine("try this" + movieNames[0].background_image);
            Debug.WriteLine(TotalAmountOfMovies);
            DisplayAlert(TotalAmountOfMovies.ToString(), "ok", "ok");
            var scroll = new ScrollView();
            scroll.Orientation = ScrollOrientation.Vertical;
            Content = scroll;

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            grid.Children.Add(new Image { Source = new Uri(movieNames[0].large_cover_image) }, 0, 0);
            grid.Children.Add(new Image { Source = new Uri(movieNames[1].large_cover_image) }, 1, 0);
            grid.Children.Add(new Image { Source = new Uri(movieNames[2].large_cover_image) }, 0, 1);
            grid.Children.Add(new Image { Source = new Uri(movieNames[3].large_cover_image) }, 1, 1);
            grid.Children.Add(new Image { Source = new Uri(movieNames[4].large_cover_image) }, 0, 2);
            grid.Children.Add(new Image { Source = new Uri(movieNames[5].large_cover_image) }, 1, 2);
            grid.Children.Add(new Image { Source = new Uri(movieNames[5].large_cover_image) }, 0, 3);


            scroll.Content = grid;
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