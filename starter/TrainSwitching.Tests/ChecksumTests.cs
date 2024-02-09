using TrainSwitching.Logic;

namespace TrainSwitching.Tests;

public class ChecksumTests
{
    [Fact]
    public void CalculateChecksum_EmptyTracks_ReturnsZero()
    {
        var station = new TrainStation();
        Assert.Equal(0, station.CalculateChecksum());
    }

    [Theory]
    [InlineData((int)WagonTypes.locomotive, 0)]
    [InlineData((int)WagonTypes.freight, 0)]
    [InlineData((int)WagonTypes.transport, 0)]
    [InlineData((int)WagonTypes.passenger, 0)]
    [InlineData((int)WagonTypes.locomotive, 5)]
    [InlineData((int)WagonTypes.freight, 5)]
    [InlineData((int)WagonTypes.transport, 5)]
    [InlineData((int)WagonTypes.passenger, 5)]
    public void CalculateChecksum_SingleWagon(int wagonType, int track)
    {
        var station = new TrainStation();
        station.Tracks[track].Wagons.Add(wagonType);
        var expected = wagonType switch
        {
            (int)WagonTypes.locomotive => 10,
            (int)WagonTypes.freight => 20,
           (int)WagonTypes.transport => 30,
            _ => 1,
        };
        Assert.Equal(expected * (track + 1), station.CalculateChecksum());
    }

    [Fact]
    public void CalculateChecksum_MultipleWagonsOnMultipleTracks_ReturnsCorrectValue()
    {
        var station = new TrainStation();
        station.Tracks[0].Wagons.Add((int)WagonTypes.locomotive);
        station.Tracks[1].Wagons.Add((int)WagonTypes.freight);
        station.Tracks[1].Wagons.Add((int)WagonTypes.transport);
        Assert.Equal(10 + 2 * (20 + 30), station.CalculateChecksum());
    }
}