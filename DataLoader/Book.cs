namespace DataLoader
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher
        {
            get; set;
        }
        public int YearOfPublication { get; set; }
        public string ImageUrlSmall { get; set; }
        public string ImageUrlMedium { get; set; }
        public string ImageUrlLarge { get; set; }


        public List<BookUserRating> Ratings { get; set; } = new List<BookUserRating>();

    }
}
