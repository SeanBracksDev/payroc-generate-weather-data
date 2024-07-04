namespace GenerateWeatherData;

using Microsoft.Extensions.Configuration;

public class PayrocGenerateWeatherData
{
    public static readonly string[] Directions = ["N", "NE", "E", "SE", "S", "SW", "W", "NW"];
    public static string? temperatureUnit = "C"; // Default is C/Celsius
    public const string WindSpeedUnit = "km/h";
    static void Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true).Build();
        LoadConfiguration(config);

        DateTime now = DateTime.UtcNow;
        DateTime startDatetime = now.AddDays(-7);
        DateTime endDatetime = now;
        GenerateWeatherDataFile(startDatetime, endDatetime, "weather_data.wis");
    }

    public static void LoadConfiguration(IConfigurationRoot config)
    {
        temperatureUnit = config["AppSettings:TemperatureUnit"];

        if (temperatureUnit == null)
        {
            Console.WriteLine("No temperature unit provided, defaulting to C (Celsius)");
            temperatureUnit = "C";
        }
        else if (temperatureUnit.Equals("celsius", StringComparison.CurrentCultureIgnoreCase)
            || temperatureUnit.Equals("c", StringComparison.CurrentCultureIgnoreCase))
        {
            temperatureUnit = "C";
        }
        else if (temperatureUnit.Equals("fahrenheit", StringComparison.CurrentCultureIgnoreCase)
            || temperatureUnit.Equals("f", StringComparison.CurrentCultureIgnoreCase))
        {
            temperatureUnit = "F";
        }
        // if no valud valid temperature unit provied set to C
        else if (temperatureUnit != "F" && temperatureUnit != "C")
        {
            Console.WriteLine("Invalid temperature unit provided, defaulting to C (Celsius)");
            temperatureUnit = "C";
        }
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

        return $"{longitude:F6}\t{latitude:F6}\t{temperature:F1}\t{temperatureUnit}\t{windSpeed:F1}\t{WindSpeedUnit}\t{windDirection}\t{precipitation}";
    }

    public static void GenerateWeatherDataFile(DateTime startDatetime, DateTime endDatetime, string outputFilePath)
    {
        DateTime currDatetime = startDatetime;

        using StreamWriter outputFile = new(outputFilePath);

        while (currDatetime < endDatetime)
        {
            outputFile.WriteLine($"{currDatetime.ToUniversalTime():yyyy-MM-dd HH:mmUTC}");

            for (int i = 0; i < 6; i++) // Unsure what the number of entries per hour should be, so I'm using the number from the first example given. This could be made to be configurable?
            {
                outputFile.WriteLine(GenerateWeatherDataEntry());
            }

            currDatetime = currDatetime.AddHours(1);
        }
        Console.WriteLine($"{(endDatetime - startDatetime).TotalDays} day(s) of weather data generated in file: '{outputFilePath}'");
    }
}
