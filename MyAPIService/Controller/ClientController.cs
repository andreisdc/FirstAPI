using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPIService.Models;

namespace MyAPIService.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientController : ControllerBase
	{

		private readonly DataContext _context;

		public ClientController(DataContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Client>>> Create(ClientDTO clientDto,[FromQuery] int userid)
		{

			var ClientUserAccount = _context.Users.Where(a => a.Id == userid).FirstOrDefault();

			var validId = await _context.Clients.FirstOrDefaultAsync(a => a.UserId == userid);

			if (validId != null)
			{
				return BadRequest("ALREADY REGISTERED");
			}

			var NewClient = new Client()
			{
				Name = clientDto.Name,
				Surname = clientDto.Surname,
				User = ClientUserAccount,
				UserId = ClientUserAccount.Id,
			};

			

			_context.Clients.Add(NewClient);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new User {Id = NewClient.Id}, NewClient);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Client>>> GetAll()
		{
			IEnumerable<Client> clients = await _context.Clients.ToListAsync();
			return Ok(clients);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Client>> Get(int id)
		{
			var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == id);
			if (client == null)
			{
				return BadRequest("User Not Found");
			}
			return Ok(client);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Client updateClient)
		{
			var client = await _context.Clients.FindAsync(id);
			if (client == null) {
				return NotFound();
			}
			

			client.Name = updateClient.Name;
			client.Surname = updateClient.Surname;


			await _context.SaveChangesAsync();

			return NoContent();
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var client = await _context.Clients.FindAsync(id);
			if (client == null) {
				return NotFound();
			}

			_context.Clients.Remove(client);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAll() {
			var client = await _context.Clients.ToListAsync();
			if (client == null) {
				return NotFound();
			}

			_context.Clients.RemoveRange(client);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
