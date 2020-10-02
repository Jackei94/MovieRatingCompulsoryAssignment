using MovieRatingsApplication.Core.Interfaces;
using MovieRatingsApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MovieRatingsApplication.Core.Services
{
    public class MovieRatingsService
    {
        private readonly IMovieRatingsRepository RatingsRepository;

        public MovieRatingsService(IMovieRatingsRepository repo)
        {
            RatingsRepository = repo;
        }

        public int NumberOfMoviesWithGrade(int grade)
        {
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Grade must be 1 - 5");
            }

            HashSet<int> movies = new HashSet<int>();
            foreach (MovieRating rating in RatingsRepository.GetAllMovieRatings())
            {
                if (rating.Grade == grade)
                {
                    movies.Add(rating.Movie);
                }
            }
            return movies.Count;
        }

        // 1.
        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            return RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Reviewer == reviewer)
                .Count();
        }

        // 2.
        public double GetAverageRateFromReviewer(int reviewer)
        {
            List<int> AllPersonalRatingFromPerson = RatingsRepository.GetAllMovieRatings().Where(r => r.Reviewer == reviewer).Select(r => r.Grade).ToList();
            double sumOfRatings = 0; 
            AllPersonalRatingFromPerson.ForEach(r => sumOfRatings += r);
            if(AllPersonalRatingFromPerson.Count > 0)
            {
                return sumOfRatings / AllPersonalRatingFromPerson.Count;
            }
            return 0;
        }

        // 3.
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            List<int> AllPersonalRatingFromPerson = RatingsRepository.GetAllMovieRatings().Where(r => r.Reviewer == reviewer && r.Grade == rate).Select(r => r.Grade).ToList();
            return AllPersonalRatingFromPerson.Count();
        }

        // 4.
        public int GetNumberOfReviews(int movie)
        {
            return RatingsRepository.GetAllMovieRatings().Where(m => m.Movie == movie).Count();
        }

        // 5.
        public double GetAverageRateOfMovie(int movie)
        {
            List<int> AllPersonalRatingFromFilm = RatingsRepository.GetAllMovieRatings().Where(m => m.Movie == movie).Select(m => m.Grade).ToList();
            double sumOfRatings = 0;
            AllPersonalRatingFromFilm.ForEach(r => sumOfRatings += r);
            if (AllPersonalRatingFromFilm.Count > 0)
            {
                return sumOfRatings / AllPersonalRatingFromFilm.Count;
            }
            return 0;
        }

        // 6.
        public int GetNumberOfRates(int movie, int rate)
        {
            List<int> AllPersonalRatingFromFilm = RatingsRepository.GetAllMovieRatings().Where(m => m.Movie == movie && m.Grade == rate).Select(r => r.Grade).ToList();
            return AllPersonalRatingFromFilm.Count();
        }

        // 7.
        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var movie5 = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Grade == 5)
                .GroupBy(r => r.Movie)
                .Select(group => new { 
                    Movie = group.Key,
                    MovieGrade5 = group.Count() 
                });

            int max5 = movie5.Max(grp => grp.MovieGrade5);

            return movie5
                .Where(grp => grp.MovieGrade5 == max5)
                .Select(grp => grp.Movie)
                .ToList();
        }

        // 8.
        public List<int> GetMostProductiveReviewers()
        {
            IList<MovieRating> ratings = RatingsRepository.GetAllMovieRatings();

            var groupedResult = from mr in ratings
                                group mr by mr.Reviewer;

            var list = groupedResult.Select(group => new { Reviewer = group.Key, UserRatingCount = group.Count() }).ToList();

            List<int> reviewersIdWithMostReviews = list.OrderByDescending(group => group.UserRatingCount).Select(x => x.Reviewer).ToList();

            return reviewersIdWithMostReviews;
        }

        // 9.
        public object GetTopRatedMovies(int amount)
        {
            IList<MovieRating> ratings = RatingsRepository.GetAllMovieRatings();

            var groupedResult = from mr in ratings
                                group mr by mr.Movie;

            var list = groupedResult.Select(group => new { Movie = group.Key, MovieAverageScore = (group.Count() == 0) ? 0 : (group.Sum(x => x.Grade) / group.Count()) }).ToList();

            List<int> AllRatedMovies = list.OrderByDescending(x => x.MovieAverageScore).Select(x => x.Movie).ToList();

           return AllRatedMovies.Take(amount).ToList();
        }

        // 10.
        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            IList<MovieRating> ratings = RatingsRepository.GetAllMovieRatings().Where(mr => mr.Reviewer == reviewer).ToList();
            return ratings.OrderByDescending(mr => mr.Grade).ThenBy(mr => mr.Date).Select(mr => mr.Movie).ToList();
        }

        // 11.
        public List<int> GetReviewersByMovie(int movie)
        {
            IList<MovieRating> ratings = RatingsRepository.GetAllMovieRatings().Where(mr => mr.Movie == movie).ToList();
            return ratings.OrderByDescending(mr => mr.Grade).ThenBy(mr => mr.Date).Select(mr => mr.Reviewer).ToList();
        }
    }
}
