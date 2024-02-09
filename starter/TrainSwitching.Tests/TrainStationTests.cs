using TrainSwitching.Logic;

namespace TrainSwitching.Tests;

public class TrainStationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void TryApplyOperation_InvalidTrackNumber_ReturnsFalse(int trackNumber)
    {
        var station = new TrainStation();
        var op = new SwitchingOperation { TrackNumber = trackNumber };
        Assert.False(station.TryApplyOperation(op));
    }

    [Fact]
    public void TryApplyOperation_InvalidDirectionForTrack9And10_ReturnsFalse()
    {
        var station = new TrainStation();
        var op = new SwitchingOperation { TrackNumber = 9, Direction = (int)Directions.east };
        Assert.False(station.TryApplyOperation(op));
    }

    [Fact]
    public void TryApplyOperation_AddOperation_AddsWagonToTrack()
    {
        var station = new TrainStation();
        var op = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.freight, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(op));
        Assert.Contains((int)WagonTypes.freight, station.Tracks[0].Wagons);
    }

    [Fact]
    public void TryApplyOperation_RemoveOperation_RemovesWagonFromTrack()
    {
        var station = new TrainStation();
        var addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.freight, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(addOp));
        var removeOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.remove, NumberOfWagons = 1, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(removeOp));
        Assert.DoesNotContain((int)WagonTypes.freight, station.Tracks[0].Wagons);
    }

    [Fact]
    public void TryApplyOperation_RemoveOperation_TooManyWagons()
    {
        var station = new TrainStation();
        var addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.freight, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(addOp));
        var removeOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.remove, NumberOfWagons = 2, Direction = (int)Directions.east };
        Assert.False(station.TryApplyOperation(removeOp));
        Assert.Contains((int)WagonTypes.freight, station.Tracks[0].Wagons);
    }

    [Fact]
    public void TryApplyOperation_RemoveOperation_RemovesFromRightDirection()
    {
        var station = new TrainStation();
        var addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.freight, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(addOp));
        addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.transport, Direction = (int)Directions.east };
        Assert.True(station.TryApplyOperation(addOp));
        var removeOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.remove, NumberOfWagons = 1, Direction = (int)Directions.west };
        Assert.True(station.TryApplyOperation(removeOp));
        Assert.DoesNotContain((int)WagonTypes.freight, station.Tracks[0].Wagons);
        Assert.Contains((int)WagonTypes.transport, station.Tracks[0].Wagons);
    }

    [Fact]
    public void TryApplyOperation_TrainLeaveOperation_ClearsTrack()
    {
        var station = new TrainStation();
        var addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.locomotive, Direction = (int)Directions.east };
        station.TryApplyOperation(addOp);
        var leaveOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.leave };
        Assert.True(station.TryApplyOperation(leaveOp));
        Assert.Empty(station.Tracks[0].Wagons);
    }

    [Fact]
    public void TryApplyOperation_TrainLeaveOperation_NoLocomotive_ReturnsFalse()
    {
        var station = new TrainStation();
        var addOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.add, WagonType = (int)WagonTypes.passenger, Direction = (int)Directions.east };
        station.TryApplyOperation(addOp);
        var leaveOp = new SwitchingOperation { TrackNumber = 1, OperationType = (int)Operations.leave };
        Assert.False(station.TryApplyOperation(leaveOp));
        Assert.NotEmpty(station.Tracks[0].Wagons);
    }
}