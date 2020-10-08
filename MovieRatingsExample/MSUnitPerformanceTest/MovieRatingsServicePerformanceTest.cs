using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRatingsApplication.Core.Data;
using MovieRatingsApplication.Core.Interfaces;
using MovieRatingsApplication.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSUnitPerformanceTest
{
    [TestClass]
    public class MovieRatingsServicePerformanceTest
    {
        private static IMovieRatingsRepository Repo;

        //Denne data kom fra TestFixture i Comp1 code samples på moodle
        private const int ReviewerWithMostReviews = 571;
        private const int MovieWithMostReviews = 305344;

        [ClassInitialize]
        public static void SetUpTest(TestContext tc)
        {
            Repo = new MovieRatingsRepository();
        }

        [TestMethod(), Timeout(4000)]
        [DataRow(1, 104874)]
        public void NumberOfMoviesWithGrade(int grade, int expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.NumberOfMoviesWithGrade(grade);

            // assert
            Assert.AreEqual(expected, result);
        }

        //  1. On input N, what are the number of reviews from reviewer N?
        [TestMethod(), Timeout(4000)]
        [DataRow(ReviewerWithMostReviews, 154832)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.GetNumberOfReviewsFromReviewer(reviewer);

            // assert
            Assert.AreEqual(expected, result);
        }

        // 2. On input N, what is the average rate that reviewer N had given?
        [TestMethod(), Timeout(4000)]
        [DataRow(ReviewerWithMostReviews, 3.9626)]
        public void GetAverageRateFromReviewer(int reviewer, double expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            double result = Double.Parse(mrs.GetAverageRateFromReviewer(reviewer).ToString("0.####"));

            // assert
            Assert.AreEqual(expected, result);
        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        [TestMethod(), Timeout(4000)]
        [DataRow(ReviewerWithMostReviews, 3, 27886)]
        public void GetNumberOfRatesByReviewer(int reviewer, int rate, int expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.GetNumberOfRatesByReviewer(reviewer, rate);

            // assert
            Assert.AreEqual(expected, result);
        }

        // 4. On input N, how many have reviewed movie N?
        [TestMethod(), Timeout(4000)]
        [DataRow(MovieWithMostReviews, 992)]
        public void GetNumberOfReviews(int movie, int expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.GetNumberOfReviews(movie);

            // assert
            Assert.AreEqual(expected, result);
        }

        // 5. On input N, what is the average rate the movie N had received?
        [TestMethod(), Timeout(4000)]
        [DataRow(MovieWithMostReviews, 1.8528225806451613)]
        public void GetAverageRateOfMovie(int movie, double expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            double result = mrs.GetAverageRateOfMovie(movie);

            // assert
            Assert.AreEqual(expected, result);
        }

        // 6. On input N and R, how many times had movie N received rate R?
        [TestMethod(), Timeout(4000)]
        [DataRow(MovieWithMostReviews, 2, 193)]
        public void GetNumberOfRates(int movie, int rate, int expected)
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            //act
            int result = mrs.GetNumberOfRates(movie, rate);

            // assert
            Assert.AreEqual(expected, result);
        }

        //  7. What is the id(s) of the movie(s) with the highest number of top rates (5)? 
        [TestMethod(), Timeout(4000)]
        [DataRow()]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            // arange
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            List<int> expected = new List<int>() { 1664010 };

            // act
            var result = mrs.GetMoviesWithHighestNumberOfTopRates();

            // assert
            CollectionAssert.AreEqual(expected, result);

        }

        // 8. What reviewer(s) had done most reviews?
        [TestMethod(), Timeout(4000)]
        [DataRow()]
        public void GetMostProductiveReviewers()
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);
            List<int> expected = new List<int>() { 571, 30, 457, 886, 758};

            // act
            var result = mrs.GetMostProductiveReviewers().GetRange(0,5);

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
        [TestMethod(), Timeout(4000)]
        [DataRow()]
        public void GetTopRatedMovies()
        {
            // arrange

            MovieRatingsService mrs = new MovieRatingsService(Repo);

            List<int> expected = new List<int>() { 822109, 317050, 383247, 400162, 60343 };

            // act
            List<int> result = mrs.GetTopRatedMovies(5);

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        // 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        [TestMethod(), Timeout(4000)]
        [DataRow()]
        public void GetTopMoviesByReviewer()
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);
            List<int> expected = new List<int>() {565510, 823941, 471578, 2137809, 912999};


            // act
            var result = mrs.GetTopMoviesByReviewer(ReviewerWithMostReviews).GetRange(0,5);

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        // 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        [TestMethod(), Timeout(4000)]
        [DataRow()]
        public void GetReviewersByMovie()
        {
            // arrange
            MovieRatingsService mrs = new MovieRatingsService(Repo);
            List<int> expected = new List<int>() {708, 199, 208, 501, 83 };

            // act
            var result = mrs.GetReviewersByMovie(MovieWithMostReviews).GetRange(0,5);

            // assert
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
