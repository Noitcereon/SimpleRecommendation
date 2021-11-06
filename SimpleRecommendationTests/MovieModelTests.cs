using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation.Models;

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
        public void MovieModel_Test1()
        {
            Assert.Fail();
        }
    }
}