using System.ComponentModel.DataAnnotations;

namespace MyAPIService.Models {
	public class User {

		public int Id { get; set; }

		public string? Username { get; set; } = string.Empty;
		public string? Password { get; set; } = string.Empty;

		[Required]
		public bool? IsOwner { get; set; }

	}
}
