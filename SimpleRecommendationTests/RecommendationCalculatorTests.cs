using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation;
using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class RecommendationCalculatorTests
    {
        private RecommendationCalculator _recommendationCalculator;
        private List<MovieModel> _movies;
        private List<UserModel> _users;

        [TestInitialize]
        public void Init()
        {
            _recommendationCalculator = new RecommendationCalculator();
            _movies = new List<MovieModel> {
                new MovieModel(1, "All Is Lost", 2013, new List<string>{"Action", "Adventure"}, 3.5, 15),
                new MovieModel(2, "The Sandpiper", 1965, new List<string>{"Drama", "Romance"}, 2, 10),
                new MovieModel(3, "Crystal Fairy & the Magical Cactus", 2013, new List<string>{"Comedy", "Adventure"}, 4, 12),
                new MovieModel(4, "Cloudy with a Chance of Meatballs 2", 2013, new List<string>{"Animation", "Children", "Comedy", "Fantasy"}, 3.5, 15),
                new MovieModel(5, "Dr. Terror's House of Horrors", 1965, new List<string>{"Horror", "Sci-Fi"}, 5, 10),
                new MovieModel(6, "Speed 2: Cruise Control",1997, new List<string>{ "Action", "Romance", "Thriller" }, 4.3, 15),
                new MovieModel(7, "Renaissance", 2006, new List<string>{"Action", "Animation", "Film-Noir", "Sci-Fi", "Thriller"}, 4, 10),
                new MovieModel(8, "Ip Man 2", 2010, new List<string>{"Action" }, 3, 10),
                new MovieModel(9, "GoldenEye", 1995,  new List<string>{"Action", "Adventure", "Thriller"}, 4.8, 25),
                new MovieModel(10, "The thing", 1982, new List<string>{"Action", "Horror", "Sci-Fi", "Thriller"}, 4.2, 20),
            };
            _users = new List<UserModel>
            {
                new UserModel(1, "Olav", new List<int>{6,7,8,9,14,22,24,29 }, new List<int>{ 8,22,24,29}),
                new UserModel(2, "Tage", new List<int>{1,4,6,8,12,15,16,17,19,22,25,27,30,32,35,40 }, new List<int>{ 1,4,8,15,19}),
                new UserModel(3, "Ida", new List<int>{2,26,34,35,38 }, new List<int>{ 26,34,38}),
                new UserModel(4, "Eivind", new List<int>{ 4,7,11,14 }, new List<int>{ 4,11 }),
                new UserModel(5, "Mia", new List<int>{ 3,4,15,21,25 }, new List<int>{ 15,25 })
            };
        }

        [TestMethod]
        public void DeterminePopularMovies_ReturnsBetween1And3Movies()
        {
            var popularMovies = _recommendationCalculator.DeterminePopularMovies(_movies, _users);

            Assert.IsTrue(popularMovies.Count > 0 && popularMovies.Count <= 3);
        }

        [TestMethod]
        public void DeterminePopularMovies_ContainsNoDuplicates()
        {
            var popularMovies = _recommendationCalculator.DeterminePopularMovies(_movies, _users);

            HashSet<int> movieIds = new HashSet<int>();

            foreach (var movie in popularMovies)
            {
                if (movieIds.Add(movie.Id) == false)
                {
                    Assert.Fail("Duplicate detected.");
                }
            }
        }
        [TestMethod]
        public void DeterminePopularMovies_ThrowsExceptionOnInsufficentData()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _recommendationCalculator.DeterminePopularMovies(
                    new List<MovieModel>(),
                    new List<UserModel>())
                );

            Assert.ThrowsException<ArgumentException>(() =>
                _recommendationCalculator.DeterminePopularMovies(
                    new List<MovieModel>
                    {
                        new MovieModel()
                    }, 
                    new List<UserModel>()));

            // Asserts that none of the movies (in the passed argument) were bought by any of the users passed as argument.
            Assert.ThrowsException<ArgumentException>(() =>
                _recommendationCalculator.DeterminePopularMovies(
                    new List<MovieModel>
                    {
                        _movies[1]
                    },
                    new List<UserModel>
                    {
                        _users[0]
                    }));
        }
    }
}
