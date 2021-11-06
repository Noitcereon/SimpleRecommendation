using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation.Models
{
    public class MovieModel
    {
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
        [Required]
        public int Id { get; init; }
        [Required][MinLength(1)]
        public string Name { get; set; }

        [Range(1800, 9999)] // First movie made was near the end of the 19th century.
        public int ReleaseYear { get; set; }
        public List<string> Keywords { get; set; }
        public double Rating { get; set; }
        public decimal Price { get; set; }
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
