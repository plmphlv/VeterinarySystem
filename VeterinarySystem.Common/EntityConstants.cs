namespace VeterinarySystem.Common
{
	public static class EntityConstants
	{
		public const int PhoneNumberMaxLenght = 15;

		public const int PhoneNumberMinLenght = 7;

		public const int HumanNameMaxLength = 50;

		public const int HumanNameMinLength = 3;

		public const int PetOwnerAddresMaxLenght = 90;

		public const int AmnimalNameMaxLength = 50;

		public const int ProcedureNameMaxLength = 60;

		public const int DescriptionMaxLength = 255;

		public const int MedicineNameMaxLength = 62;

		public const int MedicineProducerNameMaxLength = 60;

		public const int StaffPositionNameMaxLength = 30;

		public const int AnomalTypeNameMaxLength = 30;

		//<summary>
		//Barcode maximum lenght is based on the European Article Number (EAN) standard
		//standard describing barcode symbology and numbering system used in global trade to identify a specific retail product type
		//</summary>
		public const int BarcodeMaxLenght = 13;

		public const string DateFormat = "dd/MM/yyyy HH:mm";
	}
}
