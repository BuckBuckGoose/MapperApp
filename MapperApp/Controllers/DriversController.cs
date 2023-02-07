using AutoMapper;
using MapperApp.Enums;
using MapperApp.Models;
using MapperApp.Models.DTOs.Incoming;
using MapperApp.Models.DTOs.Outgoing;
using Microsoft.AspNetCore.Mvc;

namespace MapperApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private static List<Driver> drivers = new List<Driver>();
        private readonly IMapper _mapper;
        public DriversController(ILogger<DriversController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // Get all drivers
        [HttpGet]
        public IActionResult GetDrivers()
        {
            var driversResult = drivers.Where(x => x.Status == DriverStatus.Active).ToList();

            var _drivers = _mapper.Map<IEnumerable<DriverDto>>(driversResult);
            return Ok(_drivers);
        }

        [HttpPost]
        public IActionResult CreateDriver(CreateDriverDto data)
        {
            if (ModelState.IsValid)
            {
                // Automapper will take care of mapping the DTO to the Driver instance
                var _driver = _mapper.Map<Driver>(data);

                drivers.Add(_driver);

                var newDriver = _mapper.Map<DriverDto>(_driver);
                return CreatedAtAction("GetDriver", new { _driver.Id }, newDriver);
            }

            return BadRequest("Model is not valid");


        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid id) 
        { 
            var result = drivers.FirstOrDefault(x => x.Id == id);

            if (result == null) 
                return NotFound();

            var _driver = _mapper.Map<DriverDto>(result);
            
            return Ok(_driver);
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

        [HttpDelete("{id}")]
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
