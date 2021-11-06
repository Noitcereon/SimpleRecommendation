using System;
using System.Collections.Generic;
using SimpleRecommendation.Models;

namespace SimpleRecommendation
{
    public interface IRecommendationCalculator
    {
        /// <summary>
        /// Determines the top 3 popular movies based on the amount of times a movie has been purchased.
        /// </summary>
        /// <param name="movies">Movie data (at least 1 movie)</param>
        /// <param name="users">User data (at least 1 user) </param>
        /// <returns></returns>
        IList<MovieModel> DeterminePopularMovies(List<MovieModel> movies, List<UserModel> users);

        /// <summary>
        /// Recommends a specific product to a user based on their browsing history, previously purchased products and current page.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        IList<MovieModel> RecommendProductToUser(int userId);
    }
}