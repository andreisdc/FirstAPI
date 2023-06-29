using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPIService.Models;

namespace MyAPIService.Controller {
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : ControllerBase
	{
		private readonly DataContext _context;

		public OwnerController(DataContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Owner>>> Create(OwnerDTO ownerDto, [FromQuery] int userid)
		{
			var OwenrUserAccount = await _context.Users.Where(a => a.Id == userid).FirstOrDefaultAsync();

			var validId = await _context.Owners.FirstOrDefaultAsync(a => a.UserId == userid);

			if (validId != null) { 
				return BadRequest("ALREADY REGISTERED");
			}

			var NewOwner = new Owner() {
				Name = ownerDto.Name,
				Surname = ownerDto.Surname,
				User = OwenrUserAccount,
				UserId = OwenrUserAccount.Id,
			};
			
			_context.Owners.Add(NewOwner);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new Owner() { Id = NewOwner.Id }, NewOwner);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Owner>>> GetAll() {
			IEnumerable<Owner> owners = await _context.Owners.ToListAsync();
			return Ok(owners);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Owner>> Get(int id) {
			var owner = await _context.Owners.FirstOrDefaultAsync(c => c.UserId == id);
			if (owner == null) {
				return BadRequest("User Not Found");
			}
			return Ok(owner);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var owner = await _context.Owners.FindAsync(id);
			if (owner == null) {
				return NotFound();
			}

			_context.Owners.Remove(owner);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAll() {
			var owner = await _context.Owners.ToListAsync();
			if (owner == null) {
				return NotFound();
			}

			_context.Owners.RemoveRange(owner);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, OwnerDTO updateOwner) {
			var owner = await _context.Owners.FindAsync(id);
			if (owner == null) {
				return NotFound();
			}

			owner.Name = updateOwner.Name;
			owner.Surname = updateOwner.Surname;

			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
