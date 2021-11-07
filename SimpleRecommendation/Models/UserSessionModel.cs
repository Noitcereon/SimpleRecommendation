using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation.Models
{
    public class UserSessionModel
    {
        // Might be better to use a Dictionary<int, int> instead of these two values :thinking:
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public UserSessionModel()
        {

        }
        public override string ToString()
        {
            return $"{UserId}, { ProductId }";
        }
    }
}
