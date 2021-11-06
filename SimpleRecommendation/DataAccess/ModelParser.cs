using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation
{

    /// <summary>
    /// Generates models based on comma seperated data.
    /// </summary>
    public class ModelParser : IModelParser
    {
        /// <summary>
        /// Makes a Model object from a string with comma seperated values.
        /// </summary>
        /// <param name="lineOfData">A line of comma seperated values</param>
        /// <returns>An object of type T</returns>
        /// <exception cref="ArgumentException">Thrown on invalid data.</exception>
        public T GenerateModel<T>(string lineOfData) where T : class, new()
        {
            char[] seperators = ",".ToCharArray();
            string[] splitModelData = lineOfData.Split(seperators);

            T model = new();
            switch (model)
            {
                case MovieModel:
                    ThrowExceptionOnInvalidDataLength(splitModelData, columnsInModelData: 10);
                    model = (T)Convert.ChangeType(ParseMovie(splitModelData), typeof(T));
                    break;
                case UserModel:
                    ThrowExceptionOnInvalidDataLength(splitModelData, columnsInModelData: 4);
                    model = (T)Convert.ChangeType(ParseUser(splitModelData), typeof(T));
                    break;
                default:
                    throw new NotImplementedException("Cannot parse that type of model.");
            }

            return model;
        }

        private static void ThrowExceptionOnInvalidDataLength(string[] splitModelData, int columnsInModelData)
        {
            if (DataIsCorrectLength(splitModelData, columnsInModelData) == false)
            {
                throw new ArgumentException("Data is in the wrong format", nameof(splitModelData));
            }
        }

        private UserModel ParseUser(string[] splitUserData)
        {
            #region Try to convert to correct values
            if (!int.TryParse(splitUserData[0], out int id)) throw new ArgumentException("The 1st (0) column should be an integer representing id.");
            string name = splitUserData[1];
            List<int> viewedProducts = MakeIntList(splitUserData[2]);
            List<int> previouslyPurchasedProducts = MakeIntList(splitUserData[3]);
            #endregion

            UserModel user = new UserModel
            {
                Id = id,
                Name = name,
                ViewedProducts = viewedProducts,
                PreviouslyPurchasedProducts = previouslyPurchasedProducts
            };

            return user;
        }

        /// <summary>
        /// Takes a string of semicolon ; seperated integer values and adds them to a list.
        /// </summary>
        /// <param name="semiColonSeperatedIntegers">Example: "21; 531;25;1; 666;"</param>
        /// <returns></returns>
        private List<int> MakeIntList(string semiColonSeperatedIntegers)
        {
            List<int> output = new List<int>();

            string[] splitArray = semiColonSeperatedIntegers.Split(";");
            foreach (string number in splitArray)
            {
                if(!int.TryParse(number, out int result)) throw new ArgumentException("Invalid data to make List");
                output.Add(result);
            }

            return output;
        }

        private static MovieModel ParseMovie(string[] splitMovieData)
        {
            // Columns: id, name, year, keyword 1, keyword 2, keyword 3, keyword 4, keyword 5, rating, price
            #region Trying to convert to correct values
            if (!int.TryParse(splitMovieData[0], out int id)) throw new ArgumentException("The 1st (0) column should be an integer representing id.");
            string name = splitMovieData[1];
            if (!int.TryParse(splitMovieData[2], out int releaseYear)) throw new ArgumentException("The 3rd (2) colum should be an integer representing releaseYear.");
            List<string> keywords = new List<string> { splitMovieData[3].Trim(), splitMovieData[4].Trim(), splitMovieData[5].Trim(), splitMovieData[6].Trim(), splitMovieData[7].Trim() }; // 3, 4, 5, 6, 7 CAN have genre data.
            if (!double.TryParse(splitMovieData[8], out double rating)) throw new ArgumentException("The 9th (8) column should be a double representing rating.");
            if (!decimal.TryParse(splitMovieData[9], out decimal price)) throw new ArgumentException("The 10th (9) column should be a decimal representing price.");
            #endregion

            // Removes empty keyword entries.
            keywords.RemoveAll(keywordString => string.IsNullOrWhiteSpace(keywordString));

            MovieModel movie = new MovieModel
            {
                Id = id,
                Name = name,
                ReleaseYear = releaseYear,
                Keywords = keywords,
                Rating = rating,
                Price = price
            };
            return movie;
        }

        private static bool DataIsCorrectLength(string[] splitMovieData, int expectedColumns)
        {
            return splitMovieData.Length == expectedColumns;
        }
    }
}
