using FluentAssertions;
using Moq;
using MovieRatingsApplication.Core.Data;
using MovieRatingsApplication.Core.Interfaces;
using MovieRatingsApplication.Core.Model;
using MovieRatingsApplication.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsServiceTestPerformanceTest
    {
        private IMovieRatingsRepository Repo;
        private Stopwatch Stopwatch;
        private int MaxExecutionTime = 4000;

        public MovieRatingsServiceTestPerformanceTest()
        {
            Repo = new MovieRatingsRepository();
            Stopwatch = new Stopwatch();
        }

        // returns the number movies which have got the grade N

        [Theory]
        [InlineData(1, 104874)]
        [InlineData(3, 281782)]
        [InlineData(5, 280395)]
        public void NumberOfMoviesWithGrade(int grade, int expected)
        {
            // arrange
            Stopwatch.Start();
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.NumberOfMoviesWithGrade(grade);
            Stopwatch.Stop();

            // assert
            Assert.Equal(expected, result);
            Assert.True(MaxExecutionTime > Stopwatch.ElapsedMilliseconds);
        }

        //  1. On input N, what are the number of reviews from reviewer N?
        [Theory]
        [InlineData(1, 547)]
        [InlineData(417, 1465)]
        [InlineData(931, 1809)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expected)
        {
            // arrange
            Stopwatch.Start();
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            int result = mrs.GetNumberOfReviewsFromReviewer(reviewer);
            Stopwatch.Stop();

            // assert
            Assert.Equal(expected, result);
            Assert.True(MaxExecutionTime > Stopwatch.ElapsedMilliseconds);
        }

        // 2. On input N, what is the average rate that reviewer N had given?
        [Theory]
        [InlineData(2, 3.5586)]
        [InlineData(317, 3.000)]
        [InlineData(899, 3.4974)]
        public void GetAverageRateFromReviewer(int reviewer, double expected)
        {
            // arrange
            Stopwatch.Start();
            MovieRatingsService mrs = new MovieRatingsService(Repo);

            // act
            double result = Double.Parse(mrs.GetAverageRateFromReviewer(reviewer).ToString("0.####"));
            Stopwatch.Stop();

            // assert
            Assert.Equal(expected, result);
            Assert.True(MaxExecutionTime > Stopwatch.ElapsedMilliseconds);
        }

        //// 3. On input N and R, how many times has reviewer N given rate R?
        //[Theory]
        //[InlineData(1, 1, 0)]
        //[InlineData(1, 2, 1)]
        //[InlineData(2, 5, 3)]
        //public void GetNumberOfRatesByReviewer(int reviewer, int rate, int expected)
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 1, 2, DateTime.Now),
        //        new MovieRating(2, 1, 5, DateTime.Now),
        //        new MovieRating(2, 2, 5, DateTime.Now),
        //        new MovieRating(2, 3, 5, DateTime.Now),
        //        new MovieRating(3, 3, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    // act
        //    int result = mrs.GetNumberOfRatesByReviewer(reviewer, rate);

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        //// 4. On input N, how many have reviewed movie N?
        //[Theory]
        //[InlineData(1, 0)]
        //[InlineData(2, 1)]
        //[InlineData(3, 2)]
        //public void GetNumberOfReviews(int movie, int expected)
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 2, 2, DateTime.Now),
        //        new MovieRating(2, 3, 3, DateTime.Now),
        //        new MovieRating(3, 3, 4, DateTime.Now),
        //        new MovieRating(3, 4, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    // act
        //    int result = mrs.GetNumberOfReviews(movie);

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        //// 5. On input N, what is the average rate the movie N had received?
        //[Theory]
        //[InlineData(1, 0.0)]
        //[InlineData(2, 1.0)]
        //[InlineData(3, 4.5)]
        //public void GetAverageRateOfMovie(int movie, double expected)
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 2, 1, DateTime.Now),
        //        new MovieRating(2, 3, 4, DateTime.Now),
        //        new MovieRating(2, 3, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    // act
        //    double result = mrs.GetAverageRateOfMovie(movie);

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        //// 6. On input N and R, how many times had movie N received rate R?
        //[Theory]
        //[InlineData(1, 4, 0)]
        //[InlineData(2, 5, 1)]
        //[InlineData(3, 2, 2)]
        //public void GetNumberOfRates(int movie, int rate, int expected)
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 2, 5, DateTime.Now),
        //        new MovieRating(1, 3, 2, DateTime.Now),
        //        new MovieRating(2, 3, 2, DateTime.Now),
        //        new MovieRating(5, 5, 3, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    //act
        //    int result = mrs.GetNumberOfRates(movie, rate);

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        ////  7. What is the id(s) of the movie(s) with the highest number of top rates (5)? 
        //[Fact]
        //public void GetMoviesWithHighestNumberOfTopRates()
        //{
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 1, 5, DateTime.Now),
        //        new MovieRating(1, 2, 5, DateTime.Now),

        //        new MovieRating(2, 1, 4, DateTime.Now),
        //        new MovieRating(2, 2, 5, DateTime.Now),

        //        new MovieRating(2, 3, 5, DateTime.Now),
        //        new MovieRating(3, 3, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    List<int> expected = new List<int>(){ 2, 3};

        //    // act
        //    var result = mrs.GetMoviesWithHighestNumberOfTopRates();

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);

        //}

        //// 8. What reviewer(s) had done most reviews?
        //[Fact]
        //public void GetMostProductiveReviewers()
        //{
        //    // arrange

        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 1, 5, DateTime.Now),
        //        new MovieRating(1, 2, 5, DateTime.Now),
        //        new MovieRating(1, 3, 5, DateTime.Now),

        //        new MovieRating(2, 1, 4, DateTime.Now),
        //        new MovieRating(2, 2, 5, DateTime.Now),
        //        new MovieRating(2, 3, 5, DateTime.Now),
        //        new MovieRating(2, 4, 5, DateTime.Now),

        //        new MovieRating(3, 3, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    List<int> expected = new List<int>() {2,1,3};

        //    // act
        //    var result = mrs.GetMostProductiveReviewers();

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        //// 9. On input N, what is top N of movies? The score of a movie is its average rate.
        //[Fact]
        //public void GetTopRatedMovies()
        //{
        //    // arrange

        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 1, 5, DateTime.Now),
        //        new MovieRating(2, 1, 4, DateTime.Now),

        //        new MovieRating(1, 2, 3, DateTime.Now),
        //        new MovieRating(2, 2, 4, DateTime.Now),

        //        new MovieRating(1, 3, 2, DateTime.Now),
        //        new MovieRating(2, 3, 3, DateTime.Now),
        //        new MovieRating(3, 3, 5, DateTime.Now),

        //        new MovieRating(2, 4, 5, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    List<int> expected = new List<int>() { 4,1,2,3 };

        //    // act
        //    var result = mrs.GetTopRatedMovies(4);

        //    // assert
        //    Assert.Equal(expected, result);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        //}

        //// 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        //[Fact]
        //public void GetTopMoviesByReviewer()
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(2, 1, 5, DateTime.Now),
        //        new MovieRating(3, 1, 4, DateTime.Now),
        //        new MovieRating(3, 2, 5, DateTime.Now.AddDays(-1)),
        //        new MovieRating(3, 3, 5, DateTime.Now.AddDays(-2)),
        //        new MovieRating(4, 4, 4, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    List<int> expected = new List<int>() {  };
        //    List<int> expected2 = new List<int>() { 1 };
        //    List<int> expected3 = new List<int>() { 3, 2, 1 };

        //    // act
        //    var result = mrs.GetTopMoviesByReviewer(1);
        //    var result2 = mrs.GetTopMoviesByReviewer(2);
        //    var result3 = mrs.GetTopMoviesByReviewer(3);

        //    // assert
        //    Assert.Equal(expected, result);
        //    Assert.Equal(expected2, result2);
        //    Assert.Equal(expected3, result3);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Exactly(3));
        //}

        //// 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        //[Fact]
        //public void GetReviewersByMovie()
        //{
        //    // arrange
        //    ratings = new List<MovieRating>()
        //    {
        //        new MovieRating(1, 2, 3, DateTime.Now),
        //        new MovieRating(2, 3, 4, DateTime.Now),
        //        new MovieRating(3, 3, 5, DateTime.Now.AddDays(-1)),
        //        new MovieRating(4, 3, 5, DateTime.Now.AddDays(-2)),
        //        new MovieRating(5, 4, 4, DateTime.Now)
        //    };

        //    MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

        //    List<int> expected = new List<int>() {  };
        //    List<int> expected2 = new List<int>() { 1 };
        //    List<int> expected3 = new List<int>() { 4, 3, 2 };

        //    // act
        //    var result = mrs.GetReviewersByMovie(1);
        //    var result2 = mrs.GetReviewersByMovie(2);
        //    var result3 = mrs.GetReviewersByMovie(3);

        //    // assert
        //    Assert.Equal(expected, result);
        //    Assert.Equal(expected2, result2);
        //    Assert.Equal(expected3, result3);
        //    repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Exactly(3));
        //}
    }
}
