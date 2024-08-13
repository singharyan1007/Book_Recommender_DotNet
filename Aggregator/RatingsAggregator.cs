using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLoader;

namespace Aggregator
{
    public class RatingsAggregator : IRatingsAggregator
    {
        public Dictionary<string, List<int>> Aggregate(BookDetails bookDetails, Preference preference)
        {
            Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
            HashSet<int> userId = new HashSet<int>();
            //Creates a set of unique users of preferred state and age group

            foreach (User user in bookDetails.Users)
            {
                int[] age = AgeGroup(preference.Age);
                int minAge = age[0];
                int maxAge = age[1];

                if (user.State == preference.State && user.Age >= minAge && user.Age <= maxAge)
                {
                    userId.Add(user.UserId);
                }
            }



            //we return ISBN and the list of ratings
            foreach(var rating in bookDetails.BookUserRatings)
            {
                if (userId.Contains(rating.UserID)){
                    if (dictionary.ContainsKey(rating.ISBN))
                    {
                        dictionary[rating.ISBN].Add(rating.Rating);
                    }
                    else
                    {
                        dictionary.Add(rating.ISBN, new List<int> { rating.Rating });
                    }
                }
            }


            return dictionary;

        }


        static int[] AgeGroup(int age)
        {
            int[] ageLimit = new int[2];
            if (age >= 1 && age <= 16)
            {
                ageLimit[0] = 1;
                ageLimit[1] = 16;
            }
            else if (age >= 17 && age <= 30)
            {
                ageLimit[0] = 17;
                ageLimit[1] = 30;
            }
            else if (age >= 31 && age <= 50)
            {
                ageLimit[0] = 31;
                ageLimit[1] = 50;
            }
            else if (age >= 51 && age <= 60)
            {
                ageLimit[0] = 51;
                ageLimit[1] = 60;
            }
            else if (age >= 61 && age <= 100)
    {
                ageLimit[0] = 61;
                ageLimit[1] = 100;
            }
            return ageLimit;
        }





    }



}
