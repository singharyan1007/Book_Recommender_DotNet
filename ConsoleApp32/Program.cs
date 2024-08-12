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
            var users = new List<User>
            {
                new User { UserId = 1, Age = 15, City = "NYC", State = "NY", Country = "USA" }, // Teen Age
                new User { UserId = 2, Age = 25, City = "LA", State = "CA", Country = "USA" },  // Young Age
                new User { UserId = 3, Age = 35, City = "SF", State = "CA", Country = "USA" },  // Mid Age
                new User { UserId = 4, Age = 55, City = "Seattle", State = "WA", Country = "USA" }, // Old Age
                new User { UserId = 5, Age = 65, City = "Miami", State = "FL", Country = "USA" }   // Senior Citizens
            };

            var books = new List<Book>
            {
                new Book { ISBN = "001", Title = "Book A", Author = "Author A", YearOfPublication = 2020 },
                new Book { ISBN = "002", Title = "Book B", Author = "Author B", YearOfPublication = 2021 },
                // Add more books here
            };

            var ratings = new List<BookUserRating>
            {
                new BookUserRating { UserID = 1, ISBN = "001", Rating = 5 },
                new BookUserRating { UserID = 2, ISBN = "001", Rating = 4 },
                new BookUserRating { UserID = 3, ISBN = "001", Rating = 3 },
                new BookUserRating { UserID = 4, ISBN = "001", Rating = 2 },
                new BookUserRating { UserID = 5, ISBN = "001", Rating = 1 },
                new BookUserRating { UserID = 1, ISBN = "002", Rating = 2 },
                new BookUserRating { UserID = 2, ISBN = "002", Rating = 5 },
                // Add more ratings here
            };

            var bookDetails = new BookDetails
            {
                Books = books,
                Users = users,
                BookUserRatings = ratings
            };

            var preference = new Preference
            {
                ISBN = "001",
                Age = 25,
                State = "CA"
            };

            var ratingsAggregator = new RatingsAggregator();
            var recommender = new PearsonRecommender();
            var recommendationEngine = new AIRecommendationEngine(ratingsAggregator, recommender, bookDetails);

            var recommendedBooks = recommendationEngine.Recommend(preference, 2);

            foreach (var book in recommendedBooks)
            {
                Console.WriteLine($"Recommended Book: {book.Title} by {book.Author}");
            }
        }


    }
}

