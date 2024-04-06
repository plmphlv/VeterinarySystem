namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class AnimalDetailsModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public int Age { get; set; }

		public double Weight { get; set; }

		public string AnimalTypeName { get; set; } = string.Empty;
	}
}