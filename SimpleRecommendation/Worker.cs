using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation
{
    public class Worker
    {
        private DataReader _reader = new DataReader();

        public void PrintIntro()
        {
            try
            {
                Console.WriteLine("Welcome to the \"website\"");
                Console.WriteLine();
                PrintHeadline("DataReader Tests");

                DataReader reader = new DataReader();

                #region Parser Tryout
                foreach (var item in reader.ReadUserSessionsFromTextFile())
                {
                    Console.WriteLine(item);
                }
                
                #endregion

                #region Read Movies
                //try
                //{
                //    List<MovieModel> movies = reader.ReadMoviesFromTextFile();
                //    foreach (MovieModel movie in movies)
                //    {
                //        Console.WriteLine(movie);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine($"Failed to read Movies from txt.");
                //    if (Environment.GetEnvironmentVariable("Mode") == "Debug")
                //    {
                //        Console.WriteLine($"Exception message: {ex.Message}");
                //    }
                //}
                #endregion

                #region Read Users
                //try
                //{
                //    List<UserModel> users = reader.ReadUsersFromTextFile();
                //    foreach (UserModel user in users)
                //    {
                //        Console.WriteLine(user);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void PrintPopularProducts(IRecommendationCalculator recommender)
        {
            try
            {
                PrintHeadline("Popular movies");

                List<UserModel> users = _reader.ReadUsersFromTextFile();
                List<MovieModel> movies = _reader.ReadMoviesFromTextFile();

                var popularMovies = recommender.DeterminePopularMovies(movies, users);

                foreach (var movie in popularMovies)
                {
                    Console.WriteLine(movie);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void PrintUserSpecificRecommendations(IRecommendationCalculator recommender)
        {
            try
            {
                PrintHeadline("User Specific Recommendations");

                List<UserModel> users = _reader.ReadUsersFromTextFile();
                List<MovieModel> movies = _reader.ReadMoviesFromTextFile();
                List<UserSessionModel> sessions = _reader.ReadUserSessionsFromTextFile();


                foreach (var session in sessions)
                {
                    var recommendedProduct = recommender.RecommendProductToUser(session, movies, users);
                    Console.WriteLine($"{users.Find(x => x.Id == session.UserId).Name} might be interested in: {recommendedProduct}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static void PrintHeadline(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
