using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Common;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Infrastructure;
using VeterinarySystem.Core.Models.Common;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Core.Models.StaffMember;
using VeterinarySystem.Data;
using VeterinarySystem.Data.DataSeeding.Admin;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Core.Services
{
	public class ProcedureService : IProcedureService
	{
		private readonly VeterinarySystemDbContext data;

		public ProcedureService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public async Task<int> CreateNewProcedude(int id, ProcedureFormModel model)
		{
			Procedure procedure = new Procedure()
			{
				Name = model.Name,
				Description = model.Description,
				Date = model.Date,
				AnimalId = id,
				StaffMemberId = model.StaffMemberId,
			};

			data.Procedures.Add(procedure);
			await data.SaveChangesAsync();

			return procedure.Id;
		}

		public async Task<int> DeleteProcedude(int id)
		{
			Procedure? procedure = await data.Procedures
				.FirstOrDefaultAsync(x => x.Id == id);

			if (procedure == null)
			{
				throw new NullReferenceException();
			}

			int entityId = procedure.AnimalId;

			data.Procedures.Remove(procedure);
			await data.SaveChangesAsync();

			return entityId;
		}

		public async Task EditProcedude(ProcedureFormModel model, int id)
		{
			Procedure procedure = await data.Procedures
				.FirstOrDefaultAsync(p => p.Id == id);

			if (procedure == null)
			{
				throw new NullReferenceException();
			}

			procedure.Name = model.Name;
			procedure.Description = model.Description;
			procedure.Date = model.Date;
			procedure.StaffMemberId = model.StaffMemberId;

			await data.SaveChangesAsync();
		}

		public async Task<ProcedureServiceModel> GetProcedudeDetails(int id)
		{
			ProcedureServiceModel? model = await data.Procedures
				.AsNoTracking()
				.Where(p => p.Id == id)
				.Select(p => new ProcedureServiceModel()
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					Date = p.Date.ToString(EntityConstants.DateFormat),
					AnimalName = p.Animal.Name,
					StaffMemberFullName = $"{p.StaffMember.FirstName} {p.StaffMember.LastName}"
				})
				.FirstOrDefaultAsync();

			if (model == null)
			{
				throw new NullReferenceException();
			}

			return model;
		}

		public async Task<bool> ProcedureExists(int id)
		{
			bool result = await data.Procedures
				.AsNoTracking()
				.AnyAsync(x => x.Id == id);

			return result;
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

		public async Task<ProcedureFormModel> GetEditingForm(int id)
		{
			ProcedureFormModel? form = await data.Procedures
				.AsNoTracking()
				.Where(procedure => procedure.Id == id)
				.Select(procedure => new ProcedureFormModel()
				{
					Name = procedure.Name,
					Description = procedure.Description,
					Date = procedure.Date,
					StaffMemberId = procedure.StaffMemberId
				})
				.FirstOrDefaultAsync();

			if (form == null)
			{
				throw new NullReferenceException();
			}

			return form;
		}

		public async Task<DeleteViewModel> GetDeleteViewModel(int id, string controllerName)
		{
			DeleteViewModel? model = await data.Procedures
				.AsNoTracking()
				.Where(e => e.Id == id)
				.Select(e => new DeleteViewModel()
				{
					Id = e.Id,
					Name = e.Name,
					Controller = controllerName
				}
			).FirstOrDefaultAsync();

			if (model == null)
			{
				throw new NullReferenceException();
			}

			return model;
		}

		public async Task<ProcedureQueryServiceModel> GetProcedureHistory(int animalId,
			MedicalHistoryOrder order = MedicalHistoryOrder.Newest,
			int currentPage = 1,
			int proceduresPerPage = 1)
		{
			IQueryable<Procedure> query = data.Procedures
				.AsNoTracking()
				.Where(prescription => prescription.AnimalId == animalId)
				.AsQueryable();

			if (order == MedicalHistoryOrder.Oldest)
			{
				query = query.OrderBy(prescription => prescription.Date);
			}
			else
			{
				query = query.OrderByDescending(prescription => prescription.Date);
			}

			int totalPrescriptions = query.Count();
			int totalPages = (int)Math.Ceiling((double)totalPrescriptions / proceduresPerPage);

			ICollection<ProcedureServiceModel> prescriptions = await query
				.Skip((currentPage - 1) * proceduresPerPage)
				.Take(proceduresPerPage)
				.Select(prescriptions => new ProcedureServiceModel()
				{
					Id = prescriptions.Id,
					Description = prescriptions.Description,
					Date = prescriptions.Date.ToString(EntityConstants.DateOnlyFormat),
					StaffMemberFullName = $"{prescriptions.StaffMember.FirstName} {prescriptions.StaffMember.LastName}"
				}).ToListAsync();

			ProcedureQueryServiceModel serviceQuery = new ProcedureQueryServiceModel()
			{
				TotalProcedures = totalPrescriptions,
				TotalPages = totalPages,
				Procedures = prescriptions
			};

			return serviceQuery;
		}
	}
}
