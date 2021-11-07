using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRecommendation.Models
{
    public class UserSessionModel
    {
        public int Id { get; init; }
        public int ProductId { get; set; }

        public UserSessionModel()
        {

        }
        public override string ToString()
        {
            return $"{Id}, { ProductId }";
        }
    }
}
