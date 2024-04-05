using System.ComponentModel.DataAnnotations;
using static VeterinarySystem.Common.EntityConstants;
using static VeterinarySystem.Common.ErrorMessages;

namespace VeterinarySystem.Core.Models.AnimalOwner
{
	public class AnimalOwnerFormModel
	{
		[Required,
		 Display(Name = "First name"),
		 StringLength(HumanNameMaxLength,
		 MinimumLength = HumanNameMinLength,
		 ErrorMessage = NameLenghtError)]
		public string FirstName { get; set; } = null!;

		[Required,
		 Display(Name = "Last name"),
		 StringLength(HumanNameMaxLength,
		 MinimumLength = HumanNameMinLength,
		 ErrorMessage = NameLenghtError)]
		public string LastName { get; set; } = null!;

		[Required,
		 Display(Name = "Phone number"),
		 StringLength(PhoneNumberMaxLenght,
		 MinimumLength = PhoneNumberMinLenght,
		 ErrorMessage = PhoneNumberLenghtError)]
		public string PhoneNumber { get; set; } = null!;
	}
}
