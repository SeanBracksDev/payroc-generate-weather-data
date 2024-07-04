namespace GenerateWeatherData;

public class PayrocGenerateWeatherData
{
    static readonly string[] Directions = ["N", "NE", "E", "SE", "S", "SW", "W", "NW"];
    const string TemperatureUnit = "C"; // TODO: Make this configurable - Celsius/Fahrenheit
    const string WindSpeedUnit = "km/h";

    static void Main(string[] args)
    {
        // TODO: Dummy dates to test for now, replace with actual dates
        DateTime startDatetime = new(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime endDatetime = startDatetime.AddDays(1);
        GenerateWeatherData(startDatetime, endDatetime, "weather_data.wis");
    }

    public static double GenerateRandomDouble(double min, double max)
    {
        Random random = new();
        return random.NextDouble() * (max - min) + min;
    }

    public static string GenerateWeatherDataEntry()
    {
        double longitude = GenerateRandomDouble(-180, 180);
        double latitude = GenerateRandomDouble(-90, 90);
        double temperature = GenerateRandomDouble(-273, 273); // ? unclear what the max temp should be here
        double windSpeed = GenerateRandomDouble(0, 400); // ? unclear what the max temp should be here
        Random random = new();
        string windDirection = Directions[random.Next(Directions.Length)];
        int precipitation = (int)GenerateRandomDouble(0, 100);

        return $"{longitude:F6}\t{latitude:F6}\t{temperature:F1}\t{TemperatureUnit}\t{windSpeed:F1}\t{WindSpeedUnit}\t{windDirection}\t{precipitation}";
    }

    public static void GenerateWeatherData(DateTime startDatetime, DateTime endDatetime, string outputFilePath)
    {
        DateTime currDatetime = startDatetime;

        using StreamWriter outputFile = new(outputFilePath);

        while (currDatetime < endDatetime)
        {
            outputFile.WriteLine($"{currDatetime.ToUniversalTime():yyyy-MM-dd HH:mmUTC}");
            outputFile.WriteLine(GenerateWeatherDataEntry());

            currDatetime = currDatetime.AddHours(1);
        }

    }
}
