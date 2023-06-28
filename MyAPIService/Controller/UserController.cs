using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPIService.Models;
using EntityState = System.Data.Entity.EntityState;

namespace MyAPIService.Controller {
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase {
		private readonly DataContext _context;

		public UserController(DataContext context) {
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<User>>> Create(User user) {
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new User { Id = user.Id }, user);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetAll() {
			IEnumerable<User> users = await _context.Users.ToListAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> Get(int id) {
			var user = await _context.Users.FindAsync(id);
			if (user == null) {
				return BadRequest("User Not Found");
			}
			return Ok(user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, User updatedUser) {
			var user = await _context.Users.FindAsync(id);
			if (user == null) {
				return NotFound();
			}

			user.Username = updatedUser.Username;
			user.Password = updatedUser.Password;


			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var user = await _context.Users.FindAsync(id);
			if (user == null) {
				return NotFound();
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAll() {
			var users = await _context.Users.ToListAsync();
			if (users == null) {
				return NotFound();
			}

			_context.Users.RemoveRange(users);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
