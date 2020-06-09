using System;
namespace VehicleManagement.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public Guid Id { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

    }
    
    public enum VehicleType
    {
        Car,
        Boat,
        Bike
    }
}
