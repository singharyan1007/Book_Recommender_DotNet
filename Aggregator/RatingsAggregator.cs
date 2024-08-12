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
            var filteredRatings = bookDetails.BookUserRatings
              .Where(r => IsInAgeGroup(bookDetails.Users.FirstOrDefault(u => u.UserId == r.UserID)?.Age ?? 0, preference.Age))
              .GroupBy(r => r.ISBN)
              .ToDictionary(g => g.Key, g => g.Select(r => r.Rating).ToList());

            return filteredRatings;
        }



        private bool IsInAgeGroup(int userAge, int preferenceAge)
        {
            return preferenceAge switch
            {
                int age when age >= 1 && age <= 16 => userAge >= 1 && userAge <= 16,
                int age when age >= 17 && age <= 30 => userAge >= 17 && userAge <= 30,
                int age when age >= 31 && age <= 50 => userAge >= 31 && userAge <= 50,
                int age when age >= 51 && age <= 60 => userAge >= 51 && userAge <= 60,
                int age when age >= 61 && age <= 100 => userAge >= 61 && userAge <= 100,
                _ => false,
            };
        }
    }



}
