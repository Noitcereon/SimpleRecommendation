using SimpleRecommendation.Models;
using System.Collections.Generic;

namespace SimpleRecommendation
{
    public interface IDataReader
    {
        List<MovieModel> ReadMoviesFromTextFile();
        List<UserSessionModel> ReadUserSessionsFromTextFile();
        List<UserModel> ReadUsersFromTextFile();
    }
}