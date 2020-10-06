using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRatingsApplication.Core.Model
{
    public class MovieRating
    {
        public int Reviewer { get; private set; }
        public int Movie { get; private set; }
        public int Grade { get; private set; }
        public DateTime Date { get; private set; }

        public MovieRating(int reviewer, int movie, int grade, DateTime date)
        {
            this.Reviewer = reviewer;
            this.Movie = movie;
            this.Grade = grade;
            this.Date = date;
        }
    }
}
