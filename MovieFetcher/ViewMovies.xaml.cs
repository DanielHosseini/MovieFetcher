using Newtonsoft.Json;
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
        private string _yifyUrl = "https://yts.ag/api/v2/list_movies.json?limit=10&sort_by=year&order_by=desc";
        private int movieNumber = 0;

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
            IList<Movy> movieObject = yifyMovies.data.movies;
            var scroll = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
            };

            Grid grid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0
            };
     

            for (int i = 0; i < TotalAmountOfMovies/2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                for (int y = 0; y < 2; y++)
                {
                    grid.Children.Add(new Image { Source = new Uri(movieObject[movieNumber].large_cover_image) }, y, i);
                    movieNumber++;
                }



            }

            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            //grid.Children.Add(new Image { Source = new Uri(movieNames[0].large_cover_image) }, 0, 0);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[1].large_cover_image) }, 1, 0);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[2].large_cover_image) }, 0, 1);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[3].large_cover_image) }, 1, 1);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[4].large_cover_image) }, 0, 2);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[5].large_cover_image) }, 1, 2);
            //grid.Children.Add(new Image { Source = new Uri(movieNames[6].large_cover_image) }, 0, 3);

            Content = scroll;
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