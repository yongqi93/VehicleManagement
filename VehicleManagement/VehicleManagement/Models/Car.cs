using System;
namespace VehicleManagement.Models
{
    public class Car : Vehicle
    {
        public Car()
        {
        }

        public string Engine { get; set; }

        public int Doors { get; set; }

        public int Wheels { get; set; }

        public BodyType BodyType { get; set; }

    }

    public enum BodyType
    {
        Hatchback,
        Sedan
    }
}
