﻿using MovieFetcher.Core;
using System;
using System.Collections.Generic;
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
        private JSONHandler jsonHandler = new JSONHandler();

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
                var YIFYMoviesResponse = await jsonHandler.ParseJsonAsync(YIFYURL);
				PopulateUiGridView(YIFYMoviesResponse);

			}
            catch (Exception )
            {
                //Throw error to UI
                throw new Exception("Fault");
            }

		}


		public void PopulateUiGridView(YIFYMovies yifyMovies)
        {
            loadingIndictor.IsRunning = false;

            var TotalAmountOfMovies = yifyMovies.data.limit;
            IList<Movy> movieObjects = yifyMovies.data.movies;
            var tapGestureRecognizer = new TapGestureRecognizer();

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
                    var image = new Image { Aspect = Aspect.Fill, Source = new Uri(movieObjects[movieNumber].large_cover_image) };
                    image.GestureRecognizers.Add(tapGestureRecognizer);
                    grid.Children.Add(image, column, row);

                    movieNumber++;
                }
            }

            Content = scroll;
            scroll.Content = grid;

            tapGestureRecognizer.Tapped += async (sender, e) =>
            {
                var image = (Image)sender;
                image.Opacity = .5;
                var movieIDTapped = grid.Children.IndexOf(image);
                var specificMovieObject = movieObjects[movieIDTapped];
                var specificPage = new SpecificView(specificMovieObject);
                await Navigation.PushAsync(new SpecificView());


                //var ViewMovies = new NavigationPage(new SpecificView());

              //  await Navigation.PopAsync();
               // await Navigation.PushAsync(ViewMovies);
                await Task.Delay(1000);
                image.Opacity = 1;
            };
        }

       
    }
}