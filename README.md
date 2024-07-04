# Payroc Generate Weather Data Techinical Exercise

## Running the app

```bash
git clone https://github.com/SeanBracksDev/payroc-generate-weather-data
cd payroc-generate-weather-data
```

```bash
dotnet run --project GenerateWeatherData
```

Sample output:
```
7 day(s) of weather data generated in file: 'weather_data.wis'
```

## Testing the app
```bash
dotnet test
```

## Config
An `appsettings.json` file can be provided for specifying the Temperature Unit to use.

> `appsettings.json` must be within the same directory you run the project from.

e.g
```json
{    
    "AppSettings": {
        "TemperatureUnit": "fahrenheit"
    }
}
```

Allowed values for `TemperatureUnit` are: `F`, `Fahrenheit`, `C` and `Celsius`. (Case insensitive)

If no valid value can be found, it will default to `C`

## Notes
- Built, run & tested using dotnet version `8.0.302`

- I wasn't sure what the number of records per hour should be, so stuck with 6. This is something I would have clarified if this was a real world scenario. This could be something else made configurable, but I was running towards the end of the total 2 hours I set myself for this.