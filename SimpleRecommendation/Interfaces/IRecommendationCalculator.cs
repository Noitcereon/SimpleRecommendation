using System;
using System.Collections.Generic;
using SimpleRecommendation.Models;

namespace SimpleRecommendation
{
    public interface IRecommendationCalculator
    {
        /// <summary>
        /// Determines the top 3 popular movies.
        /// </summary>
        /// <param name="movies">Movie data (at least 1 movie)</param>
        /// <param name="users">User data (at least 1 user) </param>
        /// <returns></returns>
        IList<MovieModel> DeterminePopularMovies(List<MovieModel> movies, List<UserModel> users);

        /// <summary>
        /// Recommends a specific product to a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        MovieModel RecommendProductToUser(UserSessionModel session, List<MovieModel> movies, List<UserModel> users);
    }
}