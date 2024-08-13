using System.Diagnostics;
using Aggregator;
using AIRecommendationEngineStep;
using AIRecommender;
using DataLoader;

namespace ConsoleApp32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting the list of recommended books");
            AIRecommendationEngine aIRecommendationEngine = new AIRecommendationEngine();
            Preference preference = new Preference
            {
                ISBN="0440234743",
               Age=40,
               State="washington"

            };

            Stopwatch sw= Stopwatch.StartNew();


            IList<Book> recommended= aIRecommendationEngine.Recommend(preference,10);
            foreach (Book book in recommended) 
            {
                Console.WriteLine($"{book.Title} {book.ISBN} {book.Author}");
                Console.WriteLine($"Time elapse {sw.ElapsedMilliseconds}");
            }
            

        }
           

    }
}

