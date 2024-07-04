namespace GenerateWeatherData;

class PayrocGenerateWeatherData
{
    static void Main(string[] args)
    {
        // TODO: Dummy dates to test for now, replace with actual dates
        DateTime startDatetime = new(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime endDatetime = startDatetime.AddDays(1);
        GenerateWeatherData(startDatetime, endDatetime, "weather_data.wis");
    }

    public static void GenerateWeatherData(DateTime startDatetime, DateTime endDatetime, string outputFilePath)
    {
        DateTime currDatetime = startDatetime;

        using StreamWriter outputFile = new(outputFilePath);

        while (currDatetime < endDatetime)
        {
            outputFile.WriteLine($"{currDatetime.ToUniversalTime():yyyy-MM-dd HH:mmUTC}");
            currDatetime = currDatetime.AddHours(1);
        }

    }
}
