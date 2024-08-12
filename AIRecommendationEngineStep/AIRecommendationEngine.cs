using DataLoader;
using AIRecommender;
using Aggregator;
namespace AIRecommendationEngineStep
{
    public class AIRecommendationEngine
    {
        private readonly IRatingsAggregator _ratingsAggregator;
        private readonly IRecommender _recommender;
        private readonly BookDetails _bookDetails;

        public AIRecommendationEngine(IRatingsAggregator ratingsAggregator, IRecommender recommender, BookDetails bookDetails)
        {
            _ratingsAggregator = ratingsAggregator;
            _recommender = recommender;
            _bookDetails = bookDetails;
        }



        public IList<Book> Recommend(Preference preference, int limit)
        {
            var aggregatedRatings = _ratingsAggregator.Aggregate(_bookDetails, preference);

            if (!aggregatedRatings.ContainsKey(preference.ISBN))
            {
                return new List<Book>();
            }

            var baseRatings = aggregatedRatings[preference.ISBN].ToArray();

            var correlations = new Dictionary<string, double>();

            foreach (var book in _bookDetails.Books)
            {
                if (book.ISBN == preference.ISBN) continue;

                if (aggregatedRatings.TryGetValue(book.ISBN, out var ratings))
                {
                    var correlation = _recommender.GetCorrelation(baseRatings, ratings.ToArray());
                    correlations[book.ISBN] = correlation;
                }
            }

            var recommendedBooks = correlations.OrderByDescending(c => c.Value)
                                               .Take(limit)
                                               .Select(c => _bookDetails.Books.First(b => b.ISBN == c.Key))
                                               .ToList();

            return recommendedBooks;
        }
    }
}
