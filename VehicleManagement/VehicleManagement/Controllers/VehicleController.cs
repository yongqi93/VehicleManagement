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
    public class VehicleController : ControllerBase
    {
        protected readonly VehicleContext context;


        public VehicleController(VehicleContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public virtual IActionResult Get()
        {

            List<Vehicle> vehicles = context.GetVehicle();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Get(string id)
        {
            var result = this.context.Vehicles.FirstOrDefault(v => v.Id.ToString().ToUpper() == id.ToUpper());

            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
