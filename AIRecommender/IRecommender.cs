using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender
{
    public interface IRecommender
    {
        double GetCorrelation(int[] baseData, int[] otherData);
    }
}
