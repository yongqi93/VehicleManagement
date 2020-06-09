using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VehicleManagement.Models
{
    public class VehicleContext : DbContext
    {
        public bool IsInitialized = false;
        
        public DbSet<Vehicle> Vehicles { get; set; }

        public VehicleContext(DbContextOptions<VehicleContext> options): base(options)
        {
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }


        public void LoadTestVehicle()
        {
            if(!IsInitialized)
            {
                Vehicle vehicle = new Car()
                {
                    Id = new Guid("b2d63ce7-b2b0-4e93-b72b-79083d81415d"),
                    VehicleType = VehicleType.Car,
                    Make = "Mazda",
                    Model = "Mazda 3",
                    Engine = "ML4",
                    Doors = 5,
                    BodyType = BodyType.Hatchback,
                    Wheels = 4
                };
                Vehicles.Add(vehicle);

                vehicle = new Car()
                {
                    Id = new Guid("557a0e76-d30c-4823-8600-5a23ea6e8838"),
                    VehicleType = VehicleType.Car,
                    Make = "Toyota",
                    Model = "Corrola",
                    Engine = "CL4",
                    Doors = 4,
                    BodyType = BodyType.Sedan,
                    Wheels = 4
                };
                Vehicles.Add(vehicle);
                SaveChanges();
                IsInitialized = true;
            }
        }


        public List<Vehicle> GetVehicle()
        {
            return Vehicles.ToList<Vehicle>();
        }

        public List<Car> GetAllCars()
        {
            var result = new List<Car>();
            var carList = Vehicles.Where(v => v.VehicleType == VehicleType.Car);
            if(carList!=null)
            {
                result.AddRange(carList.Cast<Car>());
            }
            return result;
        }

    }
}
