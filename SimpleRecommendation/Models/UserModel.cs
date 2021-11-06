using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation.Models
{
    public class UserModel
    {
        #region Constructors
        public UserModel()
        {
            ViewedProducts = new List<int>();
            PreviouslyPurchasedProducts = new List<int>();
        }
        public UserModel(int id, string name)
        {
            Id = id;
            Name = name;
            ViewedProducts = new List<int>();
            PreviouslyPurchasedProducts = new List<int>();
        }
        public UserModel(int id, string name, List<int> viewedProducts, List<int> previouslyPurchasedProducts)
        {
            Id = id;
            Name = name;
            ViewedProducts = viewedProducts;
            PreviouslyPurchasedProducts = previouslyPurchasedProducts;
        }
        #endregion

        #region Properties
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public List<int> ViewedProducts { get; set; }

        public List<int> PreviouslyPurchasedProducts { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            string viewedProductIds = "";
            string previouslyPurchasedProductsIds = "";

            viewedProductIds = ConvertListToString(viewedProductIds, ViewedProducts);

            previouslyPurchasedProductsIds = ConvertListToString(previouslyPurchasedProductsIds, PreviouslyPurchasedProducts);
            
            return $"{Id}, {Name}, {viewedProductIds}, {previouslyPurchasedProductsIds}";
        }

        private string ConvertListToString(string viewedProductIds, List<int> listToConvert)
        {
            if (listToConvert.Count > 0)
            {
                for (int i = 0; i < listToConvert.Count; i++)
                {
                    viewedProductIds += $" {listToConvert[i]}";
                }
            }

            return viewedProductIds;
        }
        #endregion

    }
}
