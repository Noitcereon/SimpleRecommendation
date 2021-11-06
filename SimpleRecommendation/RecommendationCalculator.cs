using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation
{
    public class RecommendationCalculator
    {

        /// <summary>
        /// Determines the top 3 popular movies based on the amount of times a movie has been purchased.
        /// </summary>
        /// <param name="movies">Movie data (at least 1 movie)</param>
        /// <param name="users">User data (at least 1 user) </param>
        /// <returns></returns>
        public IList<MovieModel> DeterminePopularMovies(List<MovieModel> movies, List<UserModel> users)
        {
            if (movies.Count == 0 || users.Count == 0)
            {
                throw new ArgumentException("Not enough data to determine popular movies.");
            }

            // key: movie id
            // value: amount of times it has been purchased
            IDictionary<int, int> moviePurchaseStats = new Dictionary<int, int>();
            foreach (UserModel user in users)
            {
                CountTimesMovieHasBeenPurchased(user, moviePurchaseStats);
            }

            IOrderedEnumerable<KeyValuePair<int, int>> sortedMoviePurchaseStats = moviePurchaseStats.OrderByDescending(x => x.Value);
            
            switch (moviePurchaseStats.Count)
            {
                case 1:
                    return MakeCollectionOfPopularMovies(sortedMoviePurchaseStats, movies, 1);
                case 2:
                    return MakeCollectionOfPopularMovies(sortedMoviePurchaseStats, movies, 2);
                default:
                    return MakeCollectionOfPopularMovies(sortedMoviePurchaseStats, movies, 3);
            }
        }


        /// <summary>
        /// Recommends a specific product to a user based on their browsing history, previously purchased products and current page.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<MovieModel> RecommendProductToUser(int userId)
        {
            // Note: If I had more time I would've looked into making this with Machine Learning (e.g. with Microsoft ML.NET NuGet)
            throw new NotImplementedException();
        }

        private void CountTimesMovieHasBeenPurchased(UserModel user, IDictionary<int, int> moviePurchaseStats)
        {
            foreach (int purchasedMovieId in user.PreviouslyPurchasedProducts)
            {
                if (moviePurchaseStats.ContainsKey(purchasedMovieId))
                {
                    moviePurchaseStats[purchasedMovieId]++;
                    continue;
                }

                moviePurchaseStats.Add(purchasedMovieId, 1);
            }
        }

        private IList<MovieModel> MakeCollectionOfPopularMovies(IOrderedEnumerable<KeyValuePair<int, int>> moviePurchaseStats, List<MovieModel> movies, int itemsToTake)
        {
            IList<MovieModel> popularMovies = new List<MovieModel>();
            IList<int> idsOfPurchasedMovies = moviePurchaseStats.Select(x => x.Key).Take(itemsToTake).ToList();
            foreach (int movieId in idsOfPurchasedMovies)
            {
                if (movies.Exists(x => x.Id == movieId &&
                                       !popularMovies.Contains(movies.Find(x => x.Id == movieId))))
                {
                    popularMovies.Add(movies.First(x => x.Id == movieId));
                }
            }
            if (popularMovies.Count == 0)
            {
                throw new ArgumentException("Not enough data to determine popular movies.");
            }

            return popularMovies;
        }
    }
}
