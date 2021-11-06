using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation.Models
{
    public class MovieModel
    {
        private int _releaseYear;
        private decimal _price;
        private double _rating;

        #region Constructors
        public MovieModel()
        {
            Name = "";
            Keywords = new List<string>();
        }
        public MovieModel(int id, string name, int year, List<string> keywords, double rating, decimal price)
        {
            Id = id;
            Name = name;
            ReleaseYear = year;
            Keywords = keywords;
            Rating = rating;
            Price = price;
        }
        #endregion
        #region Parameters
        public int Id { get; init; }
        public string Name { get; set; }

        public int ReleaseYear
        {
            get => _releaseYear; set
            {
                if (value > 9999 || value < 1800)
                    throw new ArgumentOutOfRangeException(nameof(_releaseYear), "ReleaseYear must be between 1800 & 9999");
                _releaseYear = value;
            }
        }
        public List<string> Keywords { get; set; }
        public double Rating
        {
            get => _rating; set
            {
                if (value < 0 || value > 5)
                    throw new ArgumentOutOfRangeException(nameof(_rating), "Rating must be between 0-5");
                _rating = value;
            }
        }
        public decimal Price
        {
            get => _price; set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_price), "Price cannot be negative.");
                _price = value;
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string keywords = "";
            if (Keywords.Count > 0)
            {

                foreach (string keyword in Keywords)
                {
                    if (keyword.Equals(Keywords.Last()))
                    {
                        keywords += $"{keyword}";
                        break;
                    }
                    keywords += $"{keyword}, ";
                }
            }
            return $"{Id}, {Name}, {ReleaseYear}, {keywords}, {Rating}, {Price}";
        }
        #endregion
    }
}
