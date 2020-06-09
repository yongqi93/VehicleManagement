using System;
using Xunit;
using VehicleManagement.Models;
using VehicleManagement.Controllers;
using Microsoft.EntityFrameworkCore;

namespace TestVehicleManagement.Controller
{
    public class CarControllerTest
    {
        private DbContextOptions<VehicleContext> options;

        public CarControllerTest()
        {
            options = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            using (var testDbContext = new VehicleContext(options))
            {
                var newCar = new Car()
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
                testDbContext.Vehicles.Add(newCar);
                testDbContext.SaveChanges();

            }

        }


        [Fact]
        public void TestCreateCar()
        {

            using(var testDbContext = new VehicleContext(options))
            {
                var car = testDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleType == VehicleType.Car);

                Assert.NotNull(car);

            }

        }
    }
}
