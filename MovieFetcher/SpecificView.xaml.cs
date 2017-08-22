using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieFetcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecificView : ContentPage
    {
        public string MovieTitle { get; private set; }
        public int Year { get; private set; }
        public string Summary { get; private set; }
        public double IMDBRating { get; private set; }
        public string CoverImageLink { get; private set; }
        private Movy _specificMovieObject;
        public Uri CoverImageUri { get; set; }
        public ImageSource ImdbLogoSource { get; set; }
        public ImageSource TomatoLogoSource { get; set; }



        public SpecificView(Movy specificMovieObject) : base()
        {
            InitializeComponent();
            _specificMovieObject = specificMovieObject;
            Title = _specificMovieObject.title;
            MovieTitle = _specificMovieObject.title;
            Year = _specificMovieObject.year;
            Summary = _specificMovieObject.summary;
            IMDBRating = _specificMovieObject.rating;
            CoverImageLink = _specificMovieObject.large_cover_image;
            CoverImageUri = new Uri(CoverImageLink);
            ImdbLogoSource = ImageSource.FromResource("MovieFetcher.Images.imdb_128.png");
            TomatoLogoSource = ImageSource.FromResource("MovieFetcher.Images.tomato_128.png");

            BindingContext = this;

        }
    }
}