using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPIService.Models;

namespace MyAPIService.Controller {
	[Route("api/[controller]")]
	[ApiController]
	public class PropertyController : ControllerBase
	{
		private readonly DataContext _context;

		public PropertyController(DataContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Property>>> GetAll() {
			IEnumerable<Property> properties = await _context.Properties.ToListAsync();
			return Ok(properties);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Property>> Get(int id) {
			var property = await _context.Properties.FirstOrDefaultAsync(c => c.Id == id);
			if (property == null) {
				return BadRequest("property Not Found");
			}
			return Ok(property);
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Property>>> Create(PropertyDTO propertyDto, [FromQuery] int clientId, [FromQuery] int ownerId) {
			var PropertyClient = await _context.Clients.Where(a => a.Id == clientId).FirstOrDefaultAsync();
			var PropertyOwner = await _context.Owners.Where(a => a.Id == ownerId).FirstOrDefaultAsync();

			if (PropertyOwner == null)
			{
				return BadRequest("NU");
			}

			var NewProperty = new Property() {
				Name = propertyDto.Name,
				Address = propertyDto.Address,
				Stats = propertyDto.Stats,
				Description = propertyDto.Description,
				Price = propertyDto.Price,
				ClientId = clientId,
				OwnerId = ownerId,
				Client = PropertyClient,
				Owner = PropertyOwner
			};

			_context.Properties.Add(NewProperty);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new Property() { Id = NewProperty.Id }, NewProperty);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Property>> Update(int id, PropertyDTO updateProperty)
		{
			var propery = await _context.Properties.FindAsync(id);

			if (propery == null)
			{
				return NotFound();
			}

			propery.Name = updateProperty.Name;
			propery.Address = updateProperty.Address;
			propery.Description = updateProperty.Description;
			propery.Price = updateProperty.Price;
			propery.Stats = updateProperty.Stats;
			propery.Type = updateProperty.Type;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var property = await _context.Properties.FindAsync(id);
			if (property == null) {
				return NotFound();
			}

			_context.Properties.Remove(property);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAll() {
			var property = await _context.Properties.ToListAsync();
			if (property == null) {
				return NotFound();
			}

			_context.Properties.RemoveRange(property);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
