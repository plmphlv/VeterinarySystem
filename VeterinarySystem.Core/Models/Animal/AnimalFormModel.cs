namespace VeterinarySystem.Core.Models.Animal
{
	public class AnimalFormModel
	{
		public string Name { get; set; } =string.Empty;
		public double Weight { get; set; }
		public int AnimalTypeId { get; set; }
		public int AnimalOwnerId { get; set; }
	}
}
