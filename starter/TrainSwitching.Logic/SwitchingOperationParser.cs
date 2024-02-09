namespace TrainSwitching.Logic;

public static class SwitchingOperationParser
{
    /// <summary>
    /// Parses a line of input into a <see cref="SwitchingOperation"/>.
    /// </summary>
    /// <param name="inputLine">Line to parse. See readme.md for details</param>
    /// <returns>The parsed switching operation</returns>
    public static SwitchingOperation Parse(string inputLine)
    {
        var switchOperation = new SwitchingOperation();
        var parts = inputLine.Split(' ');
        switchOperation.TrackNumber = int.Parse(parts[2].Replace(',', ' '));
        switchOperation.OperationType = parts[3] switch
        {
            "add" => 1,
            "remove" => -1,
            _ => 0,
        };
        switchOperation.Direction = parts[parts.Length - 1] switch
        {
            "East" => 0,
            "West" => 1,
            _ => 0,
        };
        var numberOfWagonsFound = false;
        if (int.TryParse(parts[4], out var numberOfWagons))
        {
            switchOperation.NumberOfWagons = numberOfWagons;
            numberOfWagonsFound = true;
        }
        string wagonType = numberOfWagonsFound ? parts[5] : parts[4];

        switchOperation.WagonType = wagonType switch
        {
            "Passenger" => 0,
            "Locomotive" => 1,
            "Freight" => 2,
            "Car" => 3,
            _ => null,
        };

        return switchOperation;
    }
}