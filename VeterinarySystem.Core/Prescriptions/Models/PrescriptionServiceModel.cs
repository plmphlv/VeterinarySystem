using Animal.Contracts;
using Common.Contracts;

namespace Prescriptions.Prescription
{
    public class PrescriptionServiceModel : IDescription, IAnimal
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IssueDate { get; set; } = string.Empty;
        public int AnimalId { get; set; }
        public string AnimalName { get; set; } = string.Empty;
        public string StaffName { get; set; } = string.Empty;
    }
}
