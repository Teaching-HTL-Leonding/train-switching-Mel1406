using TrainSwitching.Logic;

namespace TrainSwitching.Tests;

public class SwitchingOperationParserTests
{
    [Theory]
    [InlineData("At track 1, add Passenger Wagon from East", 1, (int)Operations.add, (int)Directions.east, (int)WagonTypes.passenger, 1)]
    [InlineData("At track 2, add Locomotive from East", 2, (int)Operations.add, (int)Directions.east, (int)WagonTypes.locomotive, 1)]
    [InlineData("At track 3, add Freight Wagon from West", 3, (int)Operations.add, (int)Directions.west, (int)WagonTypes.freight, 1)]
    [InlineData("At track 4, add Car Transport Wagon from West", 4, (int)Operations.add, (int)Directions.west, (int)WagonTypes.transport, 1)]
    [InlineData("At track 5, remove 3 wagons from East", 5, (int)Operations.remove, (int)Directions.east, null, 3)]
    public void ParseOperation(string line, int trackNumber, int operationType, int direction, int? wagonType, int numberOfWagons)
    {
        var operation = SwitchingOperationParser.Parse(line);

        Assert.Equal(trackNumber, operation.TrackNumber);
        Assert.Equal(operationType, operation.OperationType);
        Assert.Equal(direction, operation.Direction);
        Assert.Equal(wagonType, operation.WagonType);
        Assert.Equal(numberOfWagons, operation.NumberOfWagons);
    }
}