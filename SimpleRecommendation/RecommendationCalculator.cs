using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation
{
    public class RecommendationCalculator : IRecommendationCalculator
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
        /// Recommends a specific product to a user based on their currently viewed product and previously purchased genres.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MovieModel RecommendProductToUser(UserSessionModel session, List<MovieModel> movies, List<UserModel> users)
        {
            // Note: If I had more time I would've looked into making this with Machine Learning (with Microsoft ML.NET NuGet Package)

            MovieModel recommendedProduct = new MovieModel();

            IDictionary<int, MovieModel> movieDictionary = movies.ToDictionary(movie => movie.Id);

            if (movieDictionary.TryGetValue(session.ProductId, out MovieModel currentProduct) == false)
            {
                throw new KeyNotFoundException("Could not make a recommendation, because of limited data. (can't find product with that id)");
            }

            UserModel currentUser = users.Find(user => user.Id == session.UserId);
            if (currentUser == default)
            {
                throw new KeyNotFoundException("Could not find the user with the specified session.UserId");
            }

            List<MovieModel> previouslyPurchasedMovies = MakeListOfPreviousPurchases(movieDictionary, currentUser);

            List<MovieModel> filteredMovies = new List<MovieModel>();
            if (currentProduct.Keywords.Count > 0)
            {
                HashSet<MovieModel> uniqueMovies = new();

                for (int i = 0; i < currentProduct.Keywords.Count; i++)
                {
                    List<MovieModel> moviesWithTheSameGenre = FilterMoviesByGenre(movies, currentProduct, i);

                    // adding them to a hashset ensures uniqueness.
                    AddMoviesToHashSet(moviesWithTheSameGenre, ref uniqueMovies);
                }

                filteredMovies = uniqueMovies.ToList();

                // Remove currently viewed product
                filteredMovies.Remove(currentProduct);
                // Some more filtering.
                RemovePreviouslyPurchasedProducts(filteredMovies, previouslyPurchasedMovies);
            }
            if (filteredMovies.Count < 2)
            {
                return movies[0]; // fallback to a default movie.
            }
            Random random = new Random();
            int randomIndexFromFilteredMovies = random.Next(0, filteredMovies.Count() - 1);
            recommendedProduct = filteredMovies[randomIndexFromFilteredMovies];

            return recommendedProduct;
        }

        private void AddMoviesToHashSet(List<MovieModel> moviesWithTheSameGenre, ref HashSet<MovieModel> movieModels)
        {
            foreach (MovieModel movie in moviesWithTheSameGenre)
            {
                movieModels.Add(movie);
            }
        }

        private List<MovieModel> FilterMoviesByGenre(List<MovieModel> movies, MovieModel currentProduct, int i)
        {
            List<MovieModel> moviesWithTheSameGenre = movies.Where(movie => movie.Keywords.Contains(currentProduct.Keywords[i])).ToList();
            return moviesWithTheSameGenre;
        }

        private List<MovieModel> MakeListOfPreviousPurchases(IDictionary<int, MovieModel> movieDictionary, UserModel currentUser)
        {
            List<MovieModel> output = new List<MovieModel>();

            foreach (int movieId in currentUser.PreviouslyPurchasedProducts)
            {
                if (movieDictionary.TryGetValue(movieId, out MovieModel purchasedMovie) == false)
                    continue;
                output.Add(purchasedMovie);
            }

            return output;
        }

        private void RemovePreviouslyPurchasedProducts(List<MovieModel> filteredMovies, List<MovieModel> previouslyPurchasedMovies)
        {
            foreach (MovieModel movie in previouslyPurchasedMovies)
            {
                filteredMovies.Remove(movie);
            }
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
