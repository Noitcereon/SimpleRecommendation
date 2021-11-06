using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation;
using SimpleRecommendation.Models;
using System;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class ModelParserTests
    {
        private ModelParser _modelParser;

        [TestInitialize]
        public void Init()
        {
            _modelParser = new ModelParser();
        }

        [TestMethod]
        public void GenerateModel_WorksWithMovieModel()
        {
            string lineOfMovieData = "27, Our Man Flint,1965, Adventure, Comedy, Sci-Fi, , ,3.8,20";

            MovieModel movie =_modelParser.GenerateModel<MovieModel>(lineOfMovieData);

            Assert.AreEqual(27, movie.Id);
            Assert.AreEqual("Comedy", movie.Keywords[1]);
        }

        [TestMethod]
        public void GenerateModel_WorksWithUserModel()
        {
            string lineOfUserData = "12, Noitcereon, 2;26;34;35;38, 26;34;38";

            UserModel user = _modelParser.GenerateModel<UserModel>(lineOfUserData);

            Assert.AreEqual(12, user.Id);
            Assert.AreEqual("Noitcereon", user.Name);
            Assert.AreEqual(2, user.ViewedProducts[0]);
            Assert.AreEqual(26, user.PreviouslyPurchasedProducts[0]);
        }
        [TestMethod]
        public void GenerateModel_UnknownModel()
        {
            Assert.ThrowsException<NotImplementedException>(() => _modelParser.GenerateModel<ModelParser>("1, asdf, asdfa"));
        }
        [TestMethod]
        public void GenerateModel_WrongColumnLengthThrowsException()
        {
            string lineOfInvalidMovieData = "24, Our Man Flint"; // movie data is 10 columns (9 seperator commas)
            Assert.ThrowsException<ArgumentException>(() => _modelParser.GenerateModel<MovieModel>(lineOfInvalidMovieData));
        }

        [TestMethod]
        public void ParseMovie_ThrowsException()
        {
            string lineOfInvalidMovieData = "asdf, Our Man Flint,1965, Adventure, Comedy, Sci-Fi, , ,3.8,20";
            Assert.ThrowsException<ArgumentException>(() => _modelParser.GenerateModel<MovieModel>(lineOfInvalidMovieData));
        }
        [TestMethod]
        public void ParseUser_ThrowsException()
        {
            string lineOfInvalidUserData = "asdf, Some Name, 2;3;72, 7;3";
            Assert.ThrowsException<ArgumentException>(() => _modelParser.GenerateModel<UserModel>(lineOfInvalidUserData));
        }
    }
}
