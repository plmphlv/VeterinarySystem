using VeterinarySystem.Core.Contracts;

namespace VeterinarySystem.Core.Models.Common
{
	public class DeleteViewModel : IDescription
	{
		public int Id { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Controller { get; set; } = string.Empty;
	}
}
