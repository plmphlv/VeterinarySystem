using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Prescription;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Core.Tools.ExtenshionMethods;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
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


		public async Task<int> AddPrescription(PrescriptionFormModel form, int animalId)
		{
			PrescriptionCounter? counter = await data.PrescriptionCounters.FirstOrDefaultAsync();

			counter.CurrentNumber++;

			Prescription prescription = new Prescription()
			{
				Number = form.Number,
				Description = form.Description,
				IssueDate = form.IssueDate,
				AnimalId = animalId,
				StaffMemberId = form.StaffMemberId
			};

			data.Prescriptions.Add(prescription);
			await data.SaveChangesAsync();

			return prescription.Id;
		}

		public async Task<PrescriptionServiceModel> GetPrescriptionDetails(int prescriptionId)
		{
			PrescriptionServiceModel? prescriptionDetails = await data.Prescriptions
				.AsNoTracking()
				.Where(prescription => prescription.Id == prescriptionId)
				.Select(prescription => new PrescriptionServiceModel()
				{
					Id = prescription.Id,
					Number = prescription.Number,
					Description = prescription.Description,
					IssueDate = prescription.IssueDate.ToString(EntityConstants.DateOnlyFormat),
					AnimalId = prescription.AnimalId,
					AnimalName = prescription.Animal.Name,
					StaffName = $"{prescription.StaffMember.FirstName}  {prescription.StaffMember.LastName}"
				})
				.FirstOrDefaultAsync();

			return prescriptionDetails;
		}

		public async Task EditPrescription(int prescriptionId, PrescriptionFormModel form)
		{
			Prescription prescription = await data.Prescriptions.FirstOrDefaultAsync(prescription => prescription.Id == prescriptionId);

			prescription.Description = form.Description;
			prescription.StaffMemberId = form.StaffMemberId;

			await data.SaveChangesAsync();
		}

		public async Task<int> DeletePrescription(int prescriptionId)
		{
			Prescription prescription = await data.Prescriptions.FirstOrDefaultAsync(prescription => prescription.Id == prescriptionId);

			int animalId = prescription.AnimalId;

			data.Prescriptions.Remove(prescription);
			await data.SaveChangesAsync();

			return animalId;
		}

		public async Task<bool> PrescriptionExists(int prescriptionId)
		{
			bool result = await data.Prescriptions
				.AnyAsync(prescription => prescription.Id == prescriptionId);

			return result;
		}

		public async Task<PrescriptionFormModel> GetNewPrescriptionForm()
		{
			return new Models.Prescription.PrescriptionFormModel()
			{
				Number = await GetPrescriptionNumber(),
				IssueDate = DateTimeQuickTools.GetDateAndTime().Date,
			};
		}

		public async Task<PrescriptionFormModel> GetFormForEditing(int prescriptionId)
		{
			PrescriptionFormModel? form = await data.Prescriptions
				.AsNoTracking()
				.Where(prescription => prescription.Id == prescriptionId)
				.Select(prescription => new PrescriptionFormModel()
				{
					Number = prescription.Number,
					Description = prescription.Description,
					IssueDate = prescription.IssueDate
				})
				.FirstOrDefaultAsync();

			return form;
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.Prescriptions
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Name = e.Number,
					Controller = controllerName
				})
				.FirstOrDefaultAsync();

			return model;
		}

		public async Task<ICollection<StaffServiceModel>> GetStaffMembers()
		{
			ICollection<StaffServiceModel> staff = await data.Users
				.AsNoTracking()
				.Where(u => u.Email != AdminUser.AdminEmail)
				.Select(u => new StaffServiceModel()
				{
					StaffId = u.Id,
					StaffName = $"{u.FirstName} {u.LastName}"
				})
				.ToListAsync();

			return staff;
		}

		public async Task<string> GetPrescriptionNumber()
		{
			PrescriptionCounter? counter = await data.PrescriptionCounters
				.AsNoTracking()
				.FirstOrDefaultAsync();

			counter.CurrentNumber++;

			return $"{counter.CurrentNumber:D9}";
		}

		public async Task<string> CheckPrescriptionNumber(int id)
		{
			return await data.Prescriptions
				.AsNoTracking()
				.Where(prescription => prescription.Id == id)
				.Select(prescription => prescription.Number)
				.FirstOrDefaultAsync();
		}

		public async Task<DateTime> CheckPrescriptionDate(int id)
		{
			return await data.Prescriptions
				.Where(prescription => prescription.Id == id)
				.Select(prescription => prescription.IssueDate)
				.FirstOrDefaultAsync();
		}

		public async Task<PrescriptionsQueryServiceModel> GetPrescriptionHistory(int animalId, MedicalHistoryOrder PrescriptionsOrder = MedicalHistoryOrder.Newest,
			int currentPage = 1,
			int prescriptionsPerPage = 1)
		{
			IQueryable<Prescription> query = data.Prescriptions
				.AsNoTracking()
				.Where(prescription => prescription.AnimalId == animalId)
				.AsQueryable();

			if (PrescriptionsOrder == MedicalHistoryOrder.Oldest)
			{
				query = query.OrderBy(prescription => prescription.IssueDate);
			}
			else
			{
				query = query.OrderByDescending(prescription => prescription.IssueDate);
			}

			int totalPrescriptions = query.Count();
			int totalPages = (int)Math.Ceiling((double)totalPrescriptions / prescriptionsPerPage);

			ICollection<PrescriptionServiceModel> prescriptions = await query
				.Skip((currentPage - 1) * prescriptionsPerPage)
				.Take(prescriptionsPerPage)
				.Select(prescriptions => new PrescriptionServiceModel()
				{
					Id = prescriptions.Id,
					Number = prescriptions.Number,
					Description = prescriptions.Description,
					IssueDate = prescriptions.IssueDate.ToString(EntityConstants.DateOnlyFormat),
					StaffName = $"{prescriptions.StaffMember.FirstName} {prescriptions.StaffMember.LastName}"
				}).ToListAsync();

			PrescriptionsQueryServiceModel serviceQuery = new PrescriptionsQueryServiceModel()
			{
				TotalPrescriptionsCount = totalPrescriptions,
				TotalPages = totalPages,
				Prescriptions = prescriptions
			};

			return serviceQuery;
		}
	}
}
