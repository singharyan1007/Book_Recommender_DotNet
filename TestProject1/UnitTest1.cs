using AIRecommender;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCorrelation_SameLengthArrays_ReturnsCorrectCoefficient()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] baseData = { 10, 2, 3, 4, 5 };
            int[] otherData = { 5, 6, 7, 8, 9 };

            // Act
            double result = recommender.GetCorrelation(baseData, otherData);

            // Assert
            // The expected value should be calculated manually or by a trusted source
            double expected = 0.90453; // example expected value
            Assert.AreEqual(expected, result, 0.0001, "Pearson correlation coefficient did not match the expected value.");
        }

        [TestMethod]
        public void GetCorrelation_DifferentLengthArrays_AdjustsAndReturnsCorrectCoefficient()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] baseData = { 10, 2, 3, 4 };
            int[] otherData = { 0, 4, 6, 8, 10 };

            // Act
            double result = recommender.GetCorrelation(baseData, otherData);

            // Assert
            double expected = 0.89443; // example expected value
            Assert.AreEqual(expected, result, 0.0001, "Pearson correlation coefficient did not match the expected value.");
        }

        [TestMethod]
        public void GetCorrelation_ZeroAndTenCondition_ReturnsCorrectCoefficient()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] baseData = { 0, 2, 3, 4, 10 };
            int[] otherData = { 10, 4, 6, 8, 0 };

            // Act
            double result = recommender.GetCorrelation(baseData, otherData);

            // Assert
            double expected = 0.65465; // example expected value
            Assert.AreEqual(expected, result, 0.0001, "Pearson correlation coefficient did not match the expected value.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculatePearsonCorrelation_DifferentLengthArrays_ThrowsArgumentException()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] x = { 1, 2, 3 };
            int[] y = { 4, 5 };

            // Act & Assert
            recommender.GetCorrelation(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void CalculatePearsonCorrelation_ZeroDenominator_ThrowsDivideByZeroException()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] x = { 1, 1, 1 };
            int[] y = { 2, 2, 2 };

            // Act & Assert
            recommender.GetCorrelation(x, y);
        }

        [TestMethod]
        public void TrimArray_ValidInput_TrimmedCorrectly()
        {
            // Arrange
            var recommender = new PearsonRecommender();
            int[] originalArray = { 1, 2, 3, 4, 5 };
            int expectedLength = 3;

            // Act
            int[] result = PearsonRecommender.TrimArray(originalArray, expectedLength);

            // Assert
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
        }
    }
}