using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation.Models;
using System;
using System.Collections.Generic;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class MovieModelTests
    {
        MovieModel _movie;

        [TestInitialize]
        public void Init()
        {
            _movie = new MovieModel();
        }

        [TestMethod]
        public void MovieModel_EmptyConstructor()
        {
            Assert.AreEqual(0, _movie.Id);
            Assert.AreEqual("", _movie.Name);
            Assert.AreEqual(0, _movie.ReleaseYear);
            Assert.IsInstanceOfType(_movie.Keywords, typeof(List<string>));
            Assert.AreEqual(0, _movie.Keywords.Count);
            Assert.AreEqual((double)0, _movie.Rating);
            Assert.AreEqual((decimal)0, _movie.Price);
        }
        [TestMethod]
        public void MovieModel_FullConstructor()
        {
            var movie = new MovieModel(5, "test", 2000, new List<string> { "Item 1", "Item 2" }, 4.2, (decimal)5.5);

            Assert.AreEqual(5, movie.Id);
            Assert.AreEqual("test", movie.Name);
            Assert.AreEqual(2000, movie.ReleaseYear);
            Assert.IsInstanceOfType(movie.Keywords, typeof(List<string>));
            Assert.AreEqual(2, movie.Keywords.Count);
            Assert.AreEqual(4.2, movie.Rating);
            Assert.AreEqual((decimal)5.5, movie.Price);
        }
        [TestMethod]
        public void MovieModel_ReleaseYearRestrictions()
        {
            _movie.ReleaseYear = 1800;
            _movie.ReleaseYear = 9999;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _movie.ReleaseYear = -1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _movie.ReleaseYear = 1799);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _movie.ReleaseYear = 10000);
        }
        [TestMethod]
        public void MovieModel_PriceRestrictions()
        {
            _movie.Price = new decimal(0);
            _movie.Price = new decimal(9999999.9);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _movie.Price = new decimal(-0.1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _movie.Price = new decimal(-5000));
        }

    }
}