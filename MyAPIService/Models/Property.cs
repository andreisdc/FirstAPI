namespace MyAPIService.Models {
	public class Property {

		public int Id { get; set; }
		public string? Name { get; set; } = string.Empty;
		public string? Price { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
		public string? Type { get; set; } = string.Empty;
		public Owner? Owner { get; set; } = null;
		public string? Address { get; set; } = string.Empty;
		public string? Stats { get; set; } = string.Empty;

	}
}
