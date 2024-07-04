namespace GenerateWeatherData.Tests
{
    public class PayrocGenerateWeatherDataTest
    {
        [Fact]
        public void TestGenerateRandomDouble()
        {
            double min = 0;
            double max = 10;

            double result = PayrocGenerateWeatherData.GenerateRandomDouble(min, max);

            Assert.InRange(result, min, max);
        }
    }
}