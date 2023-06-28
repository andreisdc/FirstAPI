using System.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAPIService.Models;

namespace MyAPIService.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly DataContext _context;

		public UserController(DataContext context)
		{
			_context = context;
		}


		[HttpPost]

		public async Task<ActionResult<List<User>>> Create(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(await _context.Users.ToListAsync());
		}

		[HttpGet]
		public async Task<ActionResult<List<User>>> GetAll()
		{
			return Ok(await _context.Users.ToListAsync());
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<List<User>>> Get(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return BadRequest("User Not Found");
			}
			return Ok(user);
		} 


	}
}
