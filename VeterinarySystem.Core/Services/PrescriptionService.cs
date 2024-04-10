using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Appointment;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Data;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class PrescriptionService : IPrescriptionService
	{
		private readonly VeterinarySystemDbContext data;

		public PrescriptionService(VeterinarySystemDbContext _context)
		{
			data = _context;
		}

		public async Task<int> AddPrescription(PrescriptionFormModel form, int ownerId)
		{
			Prescription prescription = new Prescription()
			{
				Number = form.Number,
				Description = form.Description,
				AnimalId = form.ProcedureId,
			};

			data.Prescriptions.Add(prescription);
			await data.SaveChangesAsync();

			return prescription.Id;
		}

		public Task<PrescriptionServiceModel> GetPrescriptionDetails(int appontmentId)
		{
			throw new NotImplementedException();
		}

		public Task EditPrescription(int appontmentId, AppointmentFromModel form)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeletePrescription(int appontmentId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> PrescriptionExists(int appontmentId)
		{
			throw new NotImplementedException();
		}

		public Task<PrescriptionFromModel> GetFormForEditing(int appontmentId)
		{
			throw new NotImplementedException();
		}

		public Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			throw new NotImplementedException();
		}

		public async Task<int> GetCurremtPrescriptionNumber()
		{
			PrescriptionCounter counter = await data.PrescriptionCounters
				.FirstOrDefaultAsync();

			counter.CurrentNumber++;

			int number = counter.CurrentNumber;

			return number;
		}

		public async Task<int> GetNewPrescriptionNumber()
		{
			PrescriptionCounter counter = await data.PrescriptionCounters
				.AsNoTracking()
				.FirstOrDefaultAsync();

			return counter.CurrentNumber;
		}
	}
}
