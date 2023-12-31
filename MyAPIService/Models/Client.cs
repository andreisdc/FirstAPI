using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPIService.Models {
	public class Client {

		public int Id { get; set; }

		public string? Name { get; set; } = string.Empty;

		public string? Surname { get; set; } = string.Empty;

		public User? User { get; set; } = null;

		[ForeignKey("User")]
		public int UserId { get; set; }

	}
}
