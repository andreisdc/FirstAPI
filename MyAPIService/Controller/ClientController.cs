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
		public async Task<ActionResult<IEnumerable<Client>>> Create(Client client, int userid)
		{

			var thisClient = _context.Clients.Where(a => a.Id == userid).FirstOrDefault();

			_context.Clients.Add(client);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new User {Id = client.Id}, client);
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
			var client = await _context.Clients.FindAsync(id);
			if (client == null)
			{
				return BadRequest("User Not Found");
			}

			return Ok(client);
		}
	}
}
