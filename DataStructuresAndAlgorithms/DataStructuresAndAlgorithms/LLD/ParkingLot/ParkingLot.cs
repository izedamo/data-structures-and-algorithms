using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresAndAlgorithms.LLD.ParkingLot;

public class ParkingLot
{
    public string Name { get; set; }
    private ParkingLot _parkingLot;

    private readonly IList<Level> _levels;

    private ParkingLot()
    {
        _levels = new List<Level>();
    }

    public ParkingLot GetInstance()
    {
        if (_parkingLot == null)
        {
            _parkingLot = new ParkingLot();
        }

        return _parkingLot;
    }

    public bool ParkVehicle(Vehicle vehicle)
    {
        return true;
    }

    public void UnparkVehicle(Vehicle vehicle) { }

    public void DisplayInformation() { }
}
