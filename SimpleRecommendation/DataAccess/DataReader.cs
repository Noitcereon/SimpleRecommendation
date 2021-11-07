using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SimpleRecommendation
{
    public class DataReader : IDataReader
    {
        private string _dataDirectory = Directory.GetCurrentDirectory() + "/CaseData";
        private IModelParser _modelParser = new ModelParser();

        public List<MovieModel> ReadMoviesFromTextFile()
        {
            string productsTxtPath = _dataDirectory + "/Products.txt";

            if (File.Exists(productsTxtPath) is false)
            {
                throw new FileNotFoundException($"Could not find the file at {productsTxtPath}");
            }

            string[] dataLines = File.ReadAllLines(productsTxtPath);
            List<MovieModel> movies = new List<MovieModel>();

            foreach (string lineOfMovieData in dataLines)
            {
                MovieModel movie = _modelParser.GenerateModel<MovieModel>(lineOfMovieData);

                movies.Add(movie);
            }

            return movies;
        }

        public List<UserModel> ReadUsersFromTextFile()
        {
            string usersTxtPath = _dataDirectory + "/Users.txt";

            if (File.Exists(usersTxtPath) is false)
            {
                throw new FileNotFoundException($"Could not find the file at {usersTxtPath}");
            }

            string[] dataLines = File.ReadAllLines(usersTxtPath);
            List<UserModel> users = new List<UserModel>();

            foreach (string lineOfMovieData in dataLines)
            {
                UserModel movie = _modelParser.GenerateModel<UserModel>(lineOfMovieData);

                users.Add(movie);
            }

            return users;
        }
        public List<UserSessionModel> ReadUserSessionsFromTextFile()
        {
            string sessionTxtPath = _dataDirectory + "/CurrentUserSession.txt";

            if (File.Exists(sessionTxtPath) is false)
            {
                throw new FileNotFoundException($"Could not find the file at {sessionTxtPath}");
            }

            string[] dataLines = File.ReadAllLines(sessionTxtPath);
            List<UserSessionModel> currentSessions = new List<UserSessionModel>();

            foreach (string lineOfMovieData in dataLines)
            {
                UserSessionModel session = _modelParser.GenerateModel<UserSessionModel>(lineOfMovieData);

                currentSessions.Add(session);
            }

            return currentSessions;
        }

    }
}
