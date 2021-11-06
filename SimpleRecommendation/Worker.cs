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
        #region Temporary Data
        //private List<MovieModel> _movies = new List<MovieModel> {
        //        new MovieModel(1, "All Is Lost", 2013, new List<string>{"Action", "Adventure"}, 3.5, 15),
        //        new MovieModel(2, "The Sandpiper", 1965, new List<string>{"Drama", "Romance"}, 2, 10),
        //        new MovieModel(3, "Crystal Fairy & the Magical Cactus", 2013, new List<string>{"Comedy", "Adventure"}, 4, 12),
        //        new MovieModel(4, "Cloudy with a Chance of Meatballs 2", 2013, new List<string>{"Animation", "Children", "Comedy", "Fantasy"}, 3.5, 15),
        //        new MovieModel(5, "Dr. Terror's House of Horrors", 1965, new List<string>{"Horror", "Sci-Fi"}, 5, 10),
        //        new MovieModel(6, "Speed 2: Cruise Control",1997, new List<string>{ "Action", "Romance", "Thriller" }, 4.3, 15),
        //        new MovieModel(7, "Renaissance", 2006, new List<string>{"Action", "Animation", "Film-Noir", "Sci-Fi", "Thriller"}, 4, 10),
        //        new MovieModel(8, "Ip Man 2", 2010, new List<string>{"Action" }, 3, 10),
        //        new MovieModel(9, "GoldenEye", 1995,  new List<string>{"Action", "Adventure", "Thriller"}, 4.8, 25),
        //        new MovieModel(10, "The thing", 1982, new List<string>{"Action", "Horror", "Sci-Fi", "Thriller"}, 4.2, 20),
        //    };
        //private List<UserModel> _users = new List<UserModel>
        //    {
        //        new UserModel(1, "Olav", new List<int>{6,7,8,9,14,22,24,29 }, new List<int>{ 8,22,24,29}),
        //        new UserModel(2, "Tage", new List<int>{1,4,6,8,12,15,16,17,19,22,25,27,30,32,35,40 }, new List<int>{ 1,4,8,15,19}),
        //        new UserModel(3, "Ida", new List<int>{2,26,34,35,38 }, new List<int>{ 26,34,38}),
        //        new UserModel(4, "Eivind", new List<int>{ 4,7,11,14 }, new List<int>{ 4,11 }),
        //        new UserModel(5, "Mia", new List<int>{ 3,4,15,21,25 }, new List<int>{ 15,25 })
        //    };
        #endregion
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
                //string lineOfData = "23, Phantasm II,9000, Action, Fantasy, Horror, Sci-Fi, Thriller,3.8,20";
                //IModelParser parser = new ModelParser();
                //var movie = parser.GenerateModel<MovieModel>(lineOfData);

                //Console.WriteLine(movie);
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
        public void PrintPopularProducts(RecommendationCalculator recommender)
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

        public void PrintUserSpecificRecommendations(RecommendationCalculator recommender)
        {
            try
            {
                PrintHeadline("User Specific Recommendations");

                List<UserModel> users = _reader.ReadUsersFromTextFile();
                //List<MovieModel> movies = _reader.ReadMoviesFromTextFile();


                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Name} might be interested in: {recommender.RecommendProductToUser(user.Id)}");
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
