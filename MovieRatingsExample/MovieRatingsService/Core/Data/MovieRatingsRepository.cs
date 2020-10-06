using MovieRatingsApplication.Core.Interfaces;
using MovieRatingsApplication.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MovieRatingsApplication.Core.Data
{
    public class MovieRatingsRepository : IMovieRatingsRepository
    {
        public static List<MovieRating> MovieRatings;

        public MovieRatingsRepository()
        {
            if (MovieRatings == null)
            {
                MovieRatings = LoadData();
            }
        }

        public IList<MovieRating> GetAllMovieRatings()
        {
            return MovieRatings;
        }

        private List<MovieRating> LoadData()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string JSONtxt = File.ReadAllText(projectDirectory + "\\MovieRatingsService\\Core\\Data\\ratings.json");
            return JsonConvert.DeserializeObject<List<MovieRating>>(JSONtxt);
        }
    }
}
