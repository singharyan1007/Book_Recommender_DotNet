using DataLoader;
using AIRecommender;
using Aggregator;
using System.Diagnostics;

namespace AIRecommendationEngineStep
{
    public class AIRecommendationEngine
    {

        public int ScannedBooks = 0;
        IDataLoader DataLoader = new CSVDataLoader();
        IRecommender Recommender = new PearsonRecommender();
        IRatingsAggregator Aggregator = new RatingsAggregator();



        public List<Book> Recommend(Preference preference, int limit)
        {
            //List<Book>  books = new List<Book>();

            // BookDetails details=new CSVDataLoader().Load();

            // Dictionary<string, List<int>> aggregrate = new RatingsAggregator().Aggregate(details,preference);

            // foreach (var book in details.BookUserRatings)
            // {
            //     if (book.ISBN == preference.ISBN)
            //     {
            //         if (aggregrate.ContainsKey(preference.ISBN))
            //         {
            //             aggregrate[preference.ISBN].Add(book.Rating);
            //         }
            //     }
            //     else
            //     {
            //         aggregrate.Add(preference.ISBN, new List<int>());
            //         aggregrate[preference.ISBN].Add((int)book.Rating); //The rating is an int
            //     }
            // }


            // Dictionary<string, List<int>> baseData=new Dictionary<string, List<int>>();
            // Dictionary<string, List<int>> otherData=new Dictionary<string, List<int>>();
            // IRecommender recommender = new PearsonRecommender();

            // foreach (var item in aggregrate.Keys)
            // {
            //     if (item == preference.ISBN)
            //     {
            //         baseData[item] = aggregrate[item];
            //     }
            //     else 
            //     {
            //         otherData[item] = aggregrate[item];
            //     }
            // }

            // // Getting the pearson result
            // Dictionary<string, double> pearsonResult = new Dictionary<string, double>();
            // if (aggregrate.ContainsKey(preference.ISBN))
            // {
            //     foreach (var item in otherData)
            //     {
            //         double val = recommender.GetCorrelation(aggregrate[preference.ISBN].ToArray(), item.Value.ToArray());

            //         pearsonResult[item.Key] = val;
            //     }
            // }
            // else { Console.WriteLine("The result not found"); }


            // List<string> recommendedBooks=pearsonResult.OrderByDescending(x => x.Value).Select(x=>x.Key).ToList();

            // foreach (var item in recommendedBooks) 
            // {
            //     foreach(var book in details.Books)
            //     {
            //         if (book.ISBN == item)
            //         {
            //             books.Add(book);
            //         }
            //     }
            // }


            // return books;

            Console.WriteLine("..................BooksData loading started..................");
            Stopwatch sw = Stopwatch.StartNew();
            List<Book> recommendedBooks = new List<Book>();
            BookDetails bookDetails = DataLoader.Load();
            Console.WriteLine("...............Loading completed successfully in " + sw.ElapsedMilliseconds + " ms............");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Aggregator called for other arrays.............");
            var aggregate = Aggregator.Aggregate(bookDetails, preference);
            Console.WriteLine("--------------------Aggregator finished-----------------------");

            // base array for pearson calculation
            Console.WriteLine("Aggregator called for base array");
            int[] baseArray = aggregate[preference.ISBN].ToArray();
            Console.WriteLine("Aggregator ratings for base array");
            for (int i = 0; i < baseArray.Length; i++)
            {
                Console.Write(baseArray[i] + " ");
            }
            Console.WriteLine();
            int count = 1;
            Console.WriteLine("----------Total books to be scanned: " + bookDetails.Books.Count + "-----------");
            Console.WriteLine("---------------------------------------------------------");

            List<string> isbnlist = new List<string>();
            foreach (var kvp in aggregate)
            {
                //For a particular ISBN get the ratings
                string isbn = kvp.Key;

                if (isbn != preference.ISBN)
                {
                    int[] otherArray = aggregate[isbn].ToArray();
                    //Console.WriteLine("Calculation started for the array" + count);
                    double pearsonValue = Recommender.GetCorrelation(baseArray, otherArray);
                    //Console.WriteLine("calculation ended for the array with pearson value= "+pearsonValue);

                    //BookWithPearsonValue[book] = pearsonValue;
                    ScannedBooks++;
                    if (pearsonValue > 0.5)//good recommendation
                    {
                        isbnlist.Add(isbn);
                        //Console.WriteLine($"Recommendation {count}:\t Pearson coefficient:{pearsonValue}");
                        count++;
                    }
                    if (count > limit)
                    {
                        break;
                    }
                }

                recommendedBooks = bookDetails.Books.Where(book => isbnlist.Contains(book.ISBN)).ToList();


                
            }

            return recommendedBooks;

        }
    }
}
