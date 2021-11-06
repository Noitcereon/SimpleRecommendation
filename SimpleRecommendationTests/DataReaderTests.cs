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
            List<MovieModel> moviesList = new();

            Assert.IsInstanceOfType(movies, moviesList.GetType());
        }

        //[TestMethod]
        //public void ReadMoviesFromTextFile_FailsGracefully()
        //{
        //    List<MovieModel> movies = _dataReader.ReadMoviesFromTextFile();

        //}
    }
}
