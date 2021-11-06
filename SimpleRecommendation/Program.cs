using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SimpleRecommendation.Models;

namespace SimpleRecommendation
{
    enum Mode
    {
        Debug,
        Production,
    }
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("Mode", Mode.Debug.ToString());
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Worker worker = new Worker();
            RecommendationCalculator recommender = new RecommendationCalculator();

            worker.PrintIntro();

            Console.WriteLine();

            worker.PrintPopularProducts(recommender);
           
            Console.WriteLine();

            worker.PrintUserSpecificRecommendations(recommender);
            
            Console.ReadKey();
        }
      
    }
}

