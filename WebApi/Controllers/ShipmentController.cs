using BL.Contracts.Shipment;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShipmentController(IShipmentService shipmentService) : ControllerBase
    {
        private readonly IShipmentService _shipmentService = shipmentService;
        // GET: api/<ShipmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShipmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShipmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/<ShipmentController>
        [HttpPost("Create")]
        public async Task Post([FromBody] TbShipmentDTO dto)
        {
            await _shipmentService.CreateAsync(dto);
        }

        // PUT api/<ShipmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShipmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
