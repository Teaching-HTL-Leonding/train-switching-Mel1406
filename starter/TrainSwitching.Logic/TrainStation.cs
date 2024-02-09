
namespace TrainSwitching.Logic;
public class TrainStation
{
    public Track[] Tracks { get; }

    public TrainStation()
    {
        Tracks = new Track[10];
        for (int i = 0; i < 10; i++)
        {
            Tracks[i] = new Track();
            Tracks[i].TrackNumber = i + 1;
            Tracks[i].openSides![1] = 1;
            if (i + 1 < 9)
            {
                Tracks[i].openSides![0] = 1;
            }
        }
    }

    /// <summary>
    /// Tries to apply the given operation to the train station.
    /// </summary>
    /// <param name="op">Operation to apply</param>
    /// <returns>Returns true if the operation could be applied, otherwise false</returns>
    public bool TryApplyOperation(SwitchingOperation op)
    {
        if (op.TrackNumber < 1 || op.TrackNumber > 10 )
        {
            return false;
        }
        var track = Tracks[op.TrackNumber - 1];
        switch (op.OperationType)
        {
            case 1:
            if(track.openSides![op.Direction] == 0)
            {
                return false;
            }
                if (op.WagonType == (int)WagonTypes.locomotive)
                {
                    track.LocomotivePresent = true; 
                }
                if (op.Direction == (int)Directions.east)
                {
                    for (int i = 0; i < op.NumberOfWagons; i++)
                    {
                        track.Wagons.Insert(0, (int)op.WagonType!);
                    }
                }
                else
                {
                    for (int i = 0; i < op.NumberOfWagons; i++)
                    {
                        track.Wagons.Add((int)op.WagonType!);
                    }
                }
                return true;
            case -1:
                if (op.NumberOfWagons > track.Wagons.Count || track.openSides![op.Direction] == 0)
                {
                    return false;
                }
                if (op.Direction == (int)Directions.east)
                {
                    for (int i = 0; i < op.NumberOfWagons; i++)
                    {
                        track.Wagons.RemoveAt(0);
                    }
                }
                else
                {
                    for (int i = 0; i < op.NumberOfWagons; i++)
                    {
                        track.Wagons.RemoveAt(track.Wagons.Count - 1);
                    }
                }
                return true;
            case 0:
                if (track.Wagons.Count == 0 || track.LocomotivePresent == false)
                {
                    return false;
                }
                while(track.Wagons.Count != 0)
                {
                   track.Wagons.RemoveAt(0);
                }
                track.LocomotivePresent = false;
                return true;
        }
        return false;
    }

    /// <summary>
    /// Calculates the checksum of the train station.
    /// </summary>
    /// <returns>The calculated checksum</returns>
    /// <remarks>
    /// See readme.md for details on how to calculate the checksum.
    /// </remarks>
    public int CalculateChecksum()
    {
        var sum = 0;
        for(int i = 0; i < 10; i++)
        {
            var track = Tracks[i];
            var result = 0;
            foreach(var wagon in track.Wagons)
            {
                 result += wagon switch{
                    (int)WagonTypes.locomotive => 10,
                    (int)WagonTypes.freight => 20,
                    3 => 30,
                    _ => 1
                };   
            }
            sum += (i+1)*result;
        }
        return sum;
    }
}