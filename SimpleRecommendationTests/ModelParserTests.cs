using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation;

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
        public void TestMethod1()
        {
            // Something to test.
            Assert.Fail();
        }
    }
}
