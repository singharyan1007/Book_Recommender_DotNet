namespace AIRecommender
{
    public class PearsonRecommender : IRecommender
    {
        public double GetCorrelation(int[] baseData, int[] otherData)
        {
            int[] baseDataCopy;
            int[] newOtherArray;

            // Check if both arrays have the same length
            if (baseData.Length != otherData.Length)
            {
                // Print original arrays
                Console.WriteLine("Original Base Data: " + string.Join(", ", baseData));
                Console.WriteLine("Original Other Data: " + string.Join(", ", otherData));


                if (baseData.Length < otherData.Length)
                {
                    // Case 1: Base array is smaller
                    baseDataCopy = new int[baseData.Length];
                    Array.Copy(baseData, baseDataCopy, baseData.Length);

                    // Trim the other array to match the base array length
                    newOtherArray = TrimArray(otherData, baseData.Length);


                }
                else
                {
                    // Case 2: Other array is smaller
                    baseDataCopy = new int[baseData.Length];
                    Array.Copy(baseData, baseDataCopy, baseData.Length);

                    // Extend the other array to match the base array length
                    newOtherArray = new int[baseData.Length];
                    Array.Copy(otherData, newOtherArray, otherData.Length);
                    for (int i = otherData.Length; i < baseData.Length; i++)
                    {
                        newOtherArray[i] = 1;
                        baseDataCopy[i]++;
                    }
                }

                // Print adjusted arrays
                Console.WriteLine("Adjusted Base Data: " + string.Join(", ", baseDataCopy));
                Console.WriteLine("Adjusted Other Data: " + string.Join(", ", newOtherArray));
            }
            else
            {
                // Case 3: Arrays are the same length
                baseDataCopy = new int[baseData.Length];
                Array.Copy(baseData, baseDataCopy, baseData.Length);

                newOtherArray = new int[otherData.Length];
                Array.Copy(otherData, newOtherArray, otherData.Length);
            }

            // Ensure no element is 0 in both arrays
            for (int i = 0; i < baseDataCopy.Length; i++)
            {
                if (baseDataCopy[i] == 0 && newOtherArray[i] == 10)
                {
                    baseDataCopy[i] = 1;  // Only increment the 0 value
                }
                else if (newOtherArray[i] == 0 && baseDataCopy[i] == 10)
                {
                    newOtherArray[i] = 1;  // Only increment the 0 value
                }
                else
                {
                    if (baseDataCopy[i] == 0 || newOtherArray[i] == 0)
                    {
                        baseDataCopy[i]++;
                        newOtherArray[i]++;
                    }
                }
            }

            // Print adjusted arrays
            Console.WriteLine("Adjusted Base Data: " + string.Join(", ", baseDataCopy));
            Console.WriteLine("Adjusted Other Data: " + string.Join(", ", newOtherArray));

            // Calculate and return the Pearson Correlation Coefficient
            double pearsonCoefficient = CalculatePearsonCorrelation(baseDataCopy, newOtherArray);
            return pearsonCoefficient;
        }

       public static int[] TrimArray(int[] originalArray, int length)
        {
            int trimLength = Math.Min(length, originalArray.Length);
            int[] trimmedArray = new int[trimLength];
            Array.Copy(originalArray, trimmedArray, trimLength);
            return trimmedArray;
        }

       public static double CalculatePearsonCorrelation(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("The input arrays must have the same length.");
            }

            int n = x.Length;
            double sumX = x.Sum();
            double sumY = y.Sum();
            double sumXSquare = x.Sum(val => val * val);
            double sumYSquare = y.Sum(val => val * val);
            double sumXY = x.Zip(y, (a, b) => a * b).Sum();

            double numerator = (n * sumXY) - (sumX * sumY);
            double denominator = Math.Sqrt(((n * sumXSquare) - (sumX * sumX)) * ((n * sumYSquare) - (sumY * sumY)));

            if (denominator == 0)
            {
                throw new DivideByZeroException("The denominator is zero, which implies a division by zero.");
            }

            return numerator / denominator;
        }
    }
}

