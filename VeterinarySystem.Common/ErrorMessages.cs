namespace VeterinarySystem.Common
{
	public static class ErrorMessages
	{
		public const string RequiredError = "This field is required!";

		public const string NameLenghtError = "Name must be between {0} and {1} characters long.";

		public const string PhoneNumberLenghtError = "Invalid phone number";

		public const string AppointmentDateError = "Appointment Date was not in the correct format.";

		public const string OwnerExistsError = "This owner already exists!";

		public const string AnimalExistsError = "This animal already exists!";
	}
}
