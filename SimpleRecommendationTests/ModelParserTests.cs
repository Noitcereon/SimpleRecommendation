using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class ModelParserTests
    {
        private ModelParser _modelParse;

        [TestInitialize]
        public void Init()
        {
            _modelParse = new ModelParser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Something to test.
            Assert.Fail();
        }
    }
}
