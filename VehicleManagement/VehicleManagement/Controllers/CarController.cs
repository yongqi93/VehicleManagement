using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleManagement.Models;
using Microsoft.AspNetCore.Http;

namespace VehicleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : VehicleController
    {
        public CarController(VehicleContext context) : base(context)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override IActionResult Get()
        {
            var vehicles = context.GetVehicle();
           List<Car> result = new List<Car>();

            if(vehicles!=null)
            {
                foreach (var v in vehicles)
                {
                    if (v is Car)
                    {
                        result.Add(v as Car);
                    }
                }
            }
            else
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override IActionResult Get(string id)
        {
            var result = this.context.Vehicles.FirstOrDefault(v => v.VehicleType == VehicleType.Car && v.Id.ToString().ToUpper() == id.ToUpper()) as Car;

            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Car
        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var targetCar = context.GetAllCars().FirstOrDefault(c => c.Id == car.Id);
                if(targetCar==null)
                {
                    targetCar = context.GetAllCars().FirstOrDefault(c => c.Make == car.Make && c.Model == car.Model);
                }
                if(targetCar!=null)
                {
                    targetCar.Wheels = car.Wheels;
                    targetCar.Doors = car.Doors;
                    targetCar.Engine = car.Engine;
                    targetCar.BodyType = car.BodyType;

                    context.Update(targetCar);
                    context.SaveChanges();
                }
                else
                {
                    targetCar = new Car()
                    {
                        Id = Guid.NewGuid(),
                        Make = car.Make,
                        Model = car.Model,
                        VehicleType = VehicleType.Car,
                        Wheels = car.Wheels,
                        Doors = car.Doors,
                        Engine = car.Engine,
                        BodyType = car.BodyType,
                    };
                    context.Vehicles.Add(targetCar);
                    context.SaveChanges();
                }
                return new JsonResult(new { success = true, message = targetCar.Id });


            }
            catch (Exception ex)
            {

                return new JsonResult(new { success = false, message = ex.Message });
            }

        }

    }
}
