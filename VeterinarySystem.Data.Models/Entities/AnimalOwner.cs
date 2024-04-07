using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Common;

namespace VeterinarySystem.Data.Domain.Entities
{
	public class AnimalOwner
	{
		public AnimalOwner()
		{
			Animals = new HashSet<Animal>();
			Appointments = new HashSet<Appointment>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(EntityConstants.HumanNameMaxLength)]
		[Unicode(true)]
		public string FirstName { get; set; } = null!;

		[Required]
		[MaxLength(EntityConstants.HumanNameMaxLength)]
		[Unicode(true)]
		public string LastName { get; set; } = null!;

		[Required]
		[MaxLength(EntityConstants.PhoneNumberMaxLenght)]
		public string PhoneNumber { get; set; } = null!;

		[MaxLength(EntityConstants.PetOwnerAddresMaxLenght)]
		[Unicode(true)]
		public string? Address { get; set; }

		public ICollection<Animal> Animals { get; set; }

		public ICollection<Appointment> Appointments { get; set; }
	}
}