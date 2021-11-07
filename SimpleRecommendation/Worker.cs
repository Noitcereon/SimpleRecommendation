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
                PrintHeadline("Welcome to the \"website\"");
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
