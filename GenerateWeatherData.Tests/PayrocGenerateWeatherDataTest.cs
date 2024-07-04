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

        [Fact]
        public void TestGenerateWeatherDataEntry()
        {
            string weatherDataEntry = PayrocGenerateWeatherData.GenerateWeatherDataEntry();
            string[] weatherDataEntryParts = weatherDataEntry.Split("\t");

            Assert.Equal(8, weatherDataEntryParts.Length);
            Assert.True(double.TryParse(weatherDataEntryParts[0], out double _));
            Assert.True(double.TryParse(weatherDataEntryParts[1], out double _));
            Assert.True(double.TryParse(weatherDataEntryParts[2], out double _));
            Assert.Equal(PayrocGenerateWeatherData.TemperatureUnit, weatherDataEntryParts[3]);
            Assert.True(double.TryParse(weatherDataEntryParts[4], out double _));
            Assert.Equal(PayrocGenerateWeatherData.WindSpeedUnit, weatherDataEntryParts[5]);
            Assert.Contains(weatherDataEntryParts[6], PayrocGenerateWeatherData.Directions);
            Assert.True(int.TryParse(weatherDataEntryParts[7], out int _));
        }

        [Fact]
        public void TestGenerateWeatherFileData()
        {
            DateTime startDatetime = new(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDatetime = startDatetime.AddDays(1);
            string outputFilePath = "weather_data.wis";

            PayrocGenerateWeatherData.GenerateWeatherDataFile(startDatetime, endDatetime, outputFilePath);

            Assert.True(File.Exists(outputFilePath));

            File.Delete(outputFilePath);
        }
    }
}