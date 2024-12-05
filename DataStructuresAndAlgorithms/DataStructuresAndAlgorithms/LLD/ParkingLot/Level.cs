using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.LLD.ParkingLot;

public class Level
{
    private readonly IList<ParkingSpot> _spots;
    private readonly int _number;

    public Level(int number)
    {
        _number = number;
        _spots = new List<ParkingSpot>();
    }

    public bool ParkVehicle(Vehicle vehicle)
    {
        return true;
    }

    public bool UnparkVehicle(Vehicle vehicle)
    {
        return true;
    }
}
