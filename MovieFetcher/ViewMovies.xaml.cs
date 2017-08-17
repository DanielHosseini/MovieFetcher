using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieFetcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMovies : ContentPage
    {
        public ViewMovies()
        {
            InitializeComponent();
            
        }

        private async Task FetchMovies(object sender, System.EventArgs e)
        {

            HttpClient htClient = new HttpClient();
            var response = await htClient.GetStringAsync("https://yts.ag/api/v2/list_movies.json?limit=10&sort_by=year&order_by=desc");
            await DisplayAlert("Clicked", "Working task", "ok");
            var root = JsonConvert.DeserializeObject<YIFYMovies>(response);

            foreach (var movie in root.Data.Movies)
            {
                Debug.WriteLine(movie.Title);
                jsonLabel.Text += movie.Title;
                
            }
                


        }
    }
}