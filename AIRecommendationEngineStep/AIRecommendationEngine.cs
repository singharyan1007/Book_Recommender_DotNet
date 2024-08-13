using DataLoader;
using AIRecommender;
using Aggregator;
namespace AIRecommendationEngineStep
{
    public class AIRecommendationEngine
    {
        



        public IList<Book> Recommend(Preference preference, int limit)
        {
           List<Book>  books = new List<Book>();

            BookDetails details=new CSVDataLoader().Load();

            Dictionary<string, List<int>> aggregrate = new RatingsAggregator().Aggregate(details,preference);

            foreach (var book in details.BookUserRatings)
            {
                if (book.ISBN == preference.ISBN)
                {
                    if (aggregrate.ContainsKey(preference.ISBN))
                    {
                        aggregrate[preference.ISBN].Add(book.Rating);
                    }
                }
                else
                {
                    aggregrate.Add(preference.ISBN, new List<int>());
                    aggregrate[preference.ISBN].Add((int)book.Rating); //The rating is an int
                }
            }


            Dictionary<string, List<int>> baseData=new Dictionary<string, List<int>>();
            Dictionary<string, List<int>> otherData=new Dictionary<string, List<int>>();
            IRecommender recommender = new PearsonRecommender();

            foreach (var item in aggregrate.Keys)
            {
                if (item == preference.ISBN)
                {
                    baseData[item] = aggregrate[item];
                }
                else 
                {
                    otherData[item] = aggregrate[item];
                }
            }

            // Getting the pearson result
            Dictionary<string, double> pearsonResult = new Dictionary<string, double>();
            if (aggregrate.ContainsKey(preference.ISBN))
            {
                foreach (var item in otherData)
                {
                    double val = recommender.GetCorrelation(aggregrate[preference.ISBN].ToArray(), item.Value.ToArray());

                    pearsonResult[item.Key] = val;
                }
            }
            else { Console.WriteLine("The result not found"); }


            List<string> recommendedBooks=pearsonResult.OrderByDescending(x => x.Value).Select(x=>x.Key).ToList();

            foreach (var item in recommendedBooks) 
            {
                foreach(var book in details.Books)
                {
                    if (book.ISBN == item)
                    {
                        books.Add(book);
                    }
                }
            }


            return books;

        }
    }
}
