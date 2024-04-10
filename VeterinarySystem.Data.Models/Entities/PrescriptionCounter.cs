using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Data.Domain.Entities
{
	public class PrescriptionCounter
	{
		[Key]
		public int Id { get; set; }

		public int CurrentNumber { get; set; }
	}
}
