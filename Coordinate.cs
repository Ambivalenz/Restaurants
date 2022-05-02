namespace RestaurantWebAPI;

public record Coordinate(double Latitude, double Longitude)
{
    public static bool TryParse(String input, out Coordinate? coordinate)
    {
        coordinate = default;
        String[] splitArray = input.Split(',', 2);
        
        if (splitArray.Length != 2)
        {
            return false;
        }

        if (!double.TryParse(splitArray[0], out Double lat))
        {
            return false;
        }

        if (!double.TryParse(splitArray[1], out Double lon))
        {
            return false;
        }

        coordinate = new Coordinate(lat, lon);

        return true;
    }

    public static async ValueTask<Coordinate?> BindAsync(HttpContext context,
        ParameterInfo parameterInfo)
    {
        String input = context.GetRouteValue(parameterInfo.Name!) as String ?? String.Empty;
        TryParse(input, out var coordinate);
        return coordinate;
    }
}