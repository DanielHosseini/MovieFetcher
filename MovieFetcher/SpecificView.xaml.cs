using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string IMDB { get; private set; }
        public string CoverImageLink { get; private set; }
        public Image CoverImage { get; private set; }


        private Movy _specificMovieObject;

        public SpecificView()
        {
            InitializeComponent();
        }

        public SpecificView(Movy specificMovieObject)
        {
            _specificMovieObject = specificMovieObject;
            MovieTitle = _specificMovieObject.title;
            Year = _specificMovieObject.year;
            Summary = _specificMovieObject.summary;
            IMDB = _specificMovieObject.imdb_code;
            CoverImage = new Image
            {
                Source = CoverImageLink,
                Aspect = Aspect.AspectFill
            };
        }
    }
}