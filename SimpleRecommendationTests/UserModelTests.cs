using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRecommendation.Models;
using System.Collections.Generic;

namespace SimpleRecommendationTests
{
    [TestClass]
    public class UserModelTests
    {
        private UserModel _user;

        [TestInitialize]
        public void Init()
        {
            _user = new UserModel();
        }

        [TestMethod]
        public void UserModel_EmptyConstructor()
        {
            Assert.AreEqual(0, _user.Id);
            Assert.AreEqual("", _user.Name);
            Assert.IsInstanceOfType(_user.ViewedProducts, typeof(List<int>));
            Assert.AreEqual(0, _user.ViewedProducts.Count);
            Assert.IsInstanceOfType(_user.PreviouslyPurchasedProducts, typeof(List<int>));
            Assert.AreEqual(0, _user.PreviouslyPurchasedProducts.Count);
        }
        [TestMethod]
        public void UserModel_IdNameConstructor()
        {
            var user = new UserModel(1, "Tim Corey");

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Tim Corey", user.Name);
            Assert.IsInstanceOfType(user.ViewedProducts, typeof(List<int>));
            Assert.AreEqual(0, user.ViewedProducts.Count);
            Assert.IsInstanceOfType(_user.PreviouslyPurchasedProducts, typeof(List<int>));
            Assert.AreEqual(0, user.PreviouslyPurchasedProducts.Count);
        }
        [TestMethod]
        public void UserModel_FullConstructor()
        {
            var user = new UserModel(3, "Sue Storm", new List<int> { 1, 2 }, new List<int> { 1 });

            Assert.AreEqual(3, user.Id);
            Assert.AreEqual("Sue Storm", user.Name);
            Assert.IsInstanceOfType(user.ViewedProducts, typeof(List<int>));
            Assert.AreEqual(2, user.ViewedProducts.Count);
            Assert.IsInstanceOfType(_user.PreviouslyPurchasedProducts, typeof(List<int>));
            Assert.AreEqual(1, user.PreviouslyPurchasedProducts.Count);
        }
    }
}
