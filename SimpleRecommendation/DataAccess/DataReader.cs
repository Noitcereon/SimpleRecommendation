using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SimpleRecommendation
{
    public class DataReader
    {
        private string _dataDirectory = "./CaseData";
        private IModelParser _modelParser = new ModelParser();

        public List<MovieModel> ReadMoviesFromTextFile()
        {
            string productsTxtPath = _dataDirectory + "/Product.txt";

            if (File.Exists(productsTxtPath) is false)
            {
                throw new FileNotFoundException($"Could not find the file at {productsTxtPath}");
            }

            string[] dataLines = File.ReadAllLines(productsTxtPath);
            List<MovieModel> movies = new List<MovieModel>();

            foreach (string lineOfMovieData in dataLines)
            {
                MovieModel movie = _modelParser.GenerateModel<MovieModel>(lineOfMovieData);

                movies.Add(movie);
            }

            return movies;
        }
       
        public List<UserModel> ReadUsersFromTextFile()
        {
            string usersTxtPath = _dataDirectory + "/Users.txt";

            if (File.Exists(usersTxtPath) is false)
            {
                throw new FileNotFoundException($"Could not find the file at {usersTxtPath}");
            }

            string[] dataLines = File.ReadAllLines(usersTxtPath);
            List<UserModel> users = new List<UserModel>();

            foreach (string lineOfMovieData in dataLines)
            {
                UserModel movie = _modelParser.GenerateModel<UserModel>(lineOfMovieData);

                users.Add(movie);
            }

            return users;
        }
        public void Test()
        {

        }

        #region Went to ModelParser
        /// <summary>
        /// Makes a MovieModel object from a string with comma seperated values.
        /// </summary>
        /// <param name="lineOfMovieData">String following this format: id (numeric value), name, year, keyword 1, keyword 2, keyword 3, keyword 4, keyword 5, rating (numeric value), price</param>
        /// <returns>MovieModel</returns>
        /// <exception cref="ArgumentException">Thrown on invalid data.</exception>
        //public MovieModel GenerateMovie(string lineOfMovieData)
        //{
        //    char[] seperators = ",".ToCharArray();
        //    string[] splitMovieData = lineOfMovieData.Split(seperators);

        //    if (DataIsCorrectLength(splitMovieData) == false)
        //    {
        //        throw new ArgumentException("Data is in the wrong format", "splitMovieData");
        //    }

        //    MovieModel movie = ParseMovie(splitMovieData);

        //    return movie;
        //}

        //private static MovieModel ParseMovie(string[] splitMovieData)
        //{
        //    #region Trying to convert to correct values
        //    if (!int.TryParse(splitMovieData[0], out int id)) throw new ArgumentException("The 1st (0) column should be an integer representing id.");
        //    string name = splitMovieData[1];
        //    if (!int.TryParse(splitMovieData[2], out int releaseYear)) throw new ArgumentException("The 3rd (2) colum should be an integer representing releaseYear.");
        //    List<string> keywords = new List<string> { splitMovieData[3].Trim(), splitMovieData[4].Trim(), splitMovieData[5].Trim(), splitMovieData[6].Trim(), splitMovieData[7].Trim() }; // 3, 4, 5, 6, 7 CAN have genre data.
        //    if (!double.TryParse(splitMovieData[8], out double rating)) throw new ArgumentException("The 9th (8) column should be a double representing rating.");
        //    if (!decimal.TryParse(splitMovieData[9], out decimal price)) throw new ArgumentException("The 10th (9) column should be a decimal representing price.");
        //    #endregion

        //    // Removes empty keyword entries.
        //    keywords.RemoveAll(keywordString => string.IsNullOrWhiteSpace(keywordString));

        //    MovieModel movie = new MovieModel
        //    {
        //        Id = id,
        //        Name = name,
        //        ReleaseYear = releaseYear,
        //        Keywords = keywords,
        //        Rating = rating,
        //        Price = price
        //    };
        //    return movie;
        //}

        //private static bool DataIsCorrectLength(string[] splitMovieData)
        //{
        //    return splitMovieData.Length == 10;
        //}
        #endregion

    }
}
