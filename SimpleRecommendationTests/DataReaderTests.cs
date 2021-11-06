using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation;
using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class DataReaderTests
    {
        private DataReader _dataReader;

        [TestInitialize]
        public void Init()
        {
            _dataReader = new DataReader();
        }

        [TestMethod]
        public void ReadMoviesFromTextFile_ReturnsCorrectType()
        {
            var movies = _dataReader.ReadMoviesFromTextFile();

            Assert.IsInstanceOfType(movies, typeof(List<MovieModel>));
        }
        [TestMethod]
        public void ReadMoviesFromTextFile_ReturnsMoreThan0()
        {
            var movies = _dataReader.ReadMoviesFromTextFile();

            Assert.IsTrue(movies.Count > 0);
        }

        [TestMethod]
        public void ReadUsersFromTextFile_ReturnsCorrectType()
        {
            List<UserModel> users = _dataReader.ReadUsersFromTextFile();

            Assert.IsInstanceOfType(users, typeof(List<UserModel>));
        }
        [TestMethod]
        public void ReadUsersFromTextFile_ReturnsMoreThan0()
        {
            var users = _dataReader.ReadUsersFromTextFile();

            Assert.IsTrue(users.Count > 0);
        }
    }
}
