using System;
using System.Collections.Generic;
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
        public int TotalRunTime { get; set; }
        private Movy _specificMovieObject;
        public Uri CoverImageUri { get; set; }
        public string Quality { get; set; }
        public ImageSource ImdbLogoSource { get; set; }
        public ImageSource TomatoLogoSource { get; set; }
        public ImageSource YouTubeLogoSource { get; set; }
        public ImageSource QualityLogoSource { get; set; }
        public ImageSource RuntimeLogoSource { get; set; }
        private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        public string YoutubeIdCode { get; set; }

        public SpecificView()
        {
            InitializeComponent();
        }

        public SpecificView(Movy specificMovieObject, int movieID) : this()
        {
            _specificMovieObject = specificMovieObject;
            Title = _specificMovieObject.title;
            MovieTitle = _specificMovieObject.title;
            Year = _specificMovieObject.year;
            Summary = _specificMovieObject.summary;
            IMDBRating = _specificMovieObject.rating;
            TotalRunTime = _specificMovieObject.runtime;
            Quality = ReturnQualtiyOfMovie(_specificMovieObject.torrents);
            CoverImageLink = _specificMovieObject.large_cover_image;
            CoverImageUri = new Uri(CoverImageLink);
            YoutubeIdCode = _specificMovieObject.yt_trailer_code;
            ImdbLogoSource = ImageSource.FromResource("MovieFetcher.Images.imdb128.png");
            YouTubeLogoSource = ImageSource.FromResource("MovieFetcher.Images.youtube128.png");
            RuntimeLogoSource = ImageSource.FromResource("MovieFetcher.Images.runtime128.png");
            QualityLogoSource = ImageSource.FromResource("MovieFetcher.Images.quality128.png");
            youtubeBtn.GestureRecognizers.Add(tapGestureRecognizer);

            BindingContext = this;

            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                Device.OpenUri(new Uri("https://www.youtube.com/watch?v=" + YoutubeIdCode));
            };
        }

        private string ReturnQualtiyOfMovie(IList<Torrent> torrents)
        {
            string quality = "";
            foreach (var item in torrents)
            {
                quality += item.quality + " ";
            }
            return quality;
        }
    }
}