namespace TrainSwitching.Logic;

public class Track
{
    public List<int> Wagons { get; } = [];
    public bool LocomotivePresent { get; set; } = false;
    public int TrackNumber { get; set; }
    public int[]? openSides = { 0, 0 }; //if the first num is set it is open to east if the second is set it is open to west
}