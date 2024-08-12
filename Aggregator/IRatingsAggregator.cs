using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLoader;
namespace Aggregator
{
    public interface IRatingsAggregator
    {
        Dictionary<string, List<int>> Aggregate(BookDetails bookDetails, Preference preference);
    }
}
