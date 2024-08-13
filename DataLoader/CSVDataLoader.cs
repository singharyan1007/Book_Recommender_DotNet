using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{

    public class CSVDataLoader : IDataLoader
    {
        private const string BooksFilePath = "BX-CSV-Dump/BX-Books.csv";
        private const string UsersFilePath = "BX-CSV-Dump/BX-Users.csv";
        private const string BookRatingsFilePath = "BX-CSV-Dump/BX-Books-Ratings.csv";
        BookDetails bookDetails=new BookDetails();
        public BookDetails Load()
        {
            Parallel.Invoke(LoadBooks, LoadRatings, LoadUsers);


            return bookDetails;
        }

        public void LoadBooks()
        {

            StreamReader booksReader = new StreamReader(BooksFilePath);
            booksReader.ReadLine();
            using (booksReader)
            {
                while (!booksReader.EndOfStream)
                {
                    string delimiter = "\";\"";
                    string[] str = booksReader.ReadLine().Split(new[] { delimiter }, StringSplitOptions.None);


                    int yop = int.Parse(str[3]);
                    str[0] = str[0].Substring(1);



                    bookDetails.Books.Add(new Book
                    {
                        ISBN = str[0],
                        Title = str[1],
                        Author = str[2],
                        YearOfPublication = yop,
                        Publisher = str[4],
                        ImageUrlSmall = str[5],
                        ImageUrlMedium = str[6],
                        ImageUrlLarge = str[7]
                    });

                }
            }
        }

        public void LoadUsers()
        {

            StreamReader userReader = new StreamReader(UsersFilePath);

            using (userReader)
            {

                userReader.ReadLine();

                while (!userReader.EndOfStream)
                {
                    string line3 = userReader.ReadLine();


                    string delimiter = "\";";

                    string[] parts3 = line3.Split(new[] { delimiter }, StringSplitOptions.None);
                    parts3[0] = parts3[0].Substring(1);
                    parts3[1] = parts3[1].Substring(1);


                    User user = new User();

                    user.UserId = int.Parse(parts3[0]);

                    string loc = parts3[1];

                    List<string> locparts = loc.Split(',').Select(s => s.Trim(' ')).ToList();

                    user.City = locparts.Count >= 1 ? locparts[0] : "";
                    user.State = locparts.Count >= 2 ? locparts[1] : "";
                    user.Country = locparts.Count >= 3 ? locparts[2] : "";


                    if (parts3[2] != "NULL")
                        user.Age = int.Parse(parts3[2].Trim('"'));
                    else
                        user.Age = -1;

                    bookDetails.Users.Add(user);








                }
            }
        }

        public void LoadRatings()
        {
            StreamReader ratingReader = new StreamReader(BookRatingsFilePath);
            ratingReader.ReadLine();

            using (ratingReader)
            {
                while (!ratingReader.EndOfStream)
                {
                    string[] str = ratingReader.ReadLine().Split(';');
                    int uid = int.Parse(str[0].Trim('"'));
                    str[1] = str[1].Trim('"');

                    int rating = int.Parse(str[2].Trim('"'));

                    bookDetails.BookUserRatings.Add(new BookUserRating { UserID = uid, ISBN = str[1], Rating = rating });


                }

            }
        }






    }
}
