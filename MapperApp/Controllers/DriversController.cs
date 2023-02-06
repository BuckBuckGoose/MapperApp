using MapperApp.Enums;
using MapperApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MapperApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private static List<Driver> drivers = new List<Driver>();
        public DriversController(ILogger<DriversController> logger)
        {
            _logger = logger;
        }

        // Get all drivers
        [HttpGet]
        public IActionResult GetDrivers()
        {
            var driversResult = drivers.Where(x => x.Status == DriverStatus.Active).ToList();
            return Ok(driversResult);
        }

        [HttpPost]
        public IActionResult CreateDriver(Driver data)
        {
            if (ModelState.IsValid)
            {
                drivers.Add(data);
                return CreatedAtAction("GetDriver", new { data.Id }, data);
            }

            return BadRequest("Model is not valid");


        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid id) 
        { 
            var result = drivers.FirstOrDefault(x => x.Id == id);

            if (result == null) 
                return NotFound();
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, Driver data) 
        {
            if (id != data.Id)
            {
                return BadRequest();
            }

            var existingDriver = drivers.FirstOrDefault(x => x.Id == data.Id);
            if (existingDriver == null)
            {
                return NotFound();
            }

            existingDriver.DriverNumber = data.DriverNumber;
            existingDriver.FirstName = data.FirstName;
            existingDriver.LastName = data.LastName;
            existingDriver.WorldChampionships = data.WorldChampionships;

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteDriver(Guid id)
        {
            var driver = drivers.FirstOrDefault(x => x.Id == id);
            
            if (driver == null)
                return NotFound();

            driver.Status = DriverStatus.Inactive;

            return NoContent();
        }
    }
}
