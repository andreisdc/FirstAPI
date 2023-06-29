namespace MyAPIService.Models {
	public class Owner {
		public int Id { get; set; }

		public string? Name { get; set; } = string.Empty;

		public string? Surname { get; set; } = string.Empty;

		public User? User { get; set; } = null;

		public int UserId { get; set; }
	}
}
