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
            try
            {
                // create a menu for these books, take choice and limit for recommendations ,
                //  age and state also and
                // set isbn of preference accordingly
                // and then generate recommendations

                

                Stopwatch sw = Stopwatch.StartNew();
                Preference preference = new Preference
                {
                    //ISBN = "0440234743",
                    ISBN = "0448401738",
                    Age = 40,
                    State = "washington"
                };

                Console.WriteLine("\n---------------------------------------------------------------------");
                Console.WriteLine($"Getting Recommendations for Book with ISBN:{preference.ISBN} .....");
                Console.WriteLine("\n---------------------------------------------------------------------");
                AIRecommendationEngine aIRecommendationEngine = new AIRecommendationEngine();
                List<Book> recommendedBooks = aIRecommendationEngine.Recommend(preference, 10);
                Console.WriteLine("\n---------------------------------------------------------------------");
                Console.WriteLine("--------------------------Recommended Books--------------------------");
                foreach (Book book in recommendedBooks)
                {
                    Console.WriteLine($"\t{book.ISBN}\t{book.Title}\t");
                }
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("Total time taken for Recommendations : " + sw.ElapsedMilliseconds + " ms");
                Console.WriteLine("Total Books Scanned in order to get the recommendations: " + aIRecommendationEngine.ScannedBooks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
           

    }
}

