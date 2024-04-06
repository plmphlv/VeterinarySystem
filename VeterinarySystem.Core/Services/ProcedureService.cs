using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarySystem.Core.Contracts;
using VeterinarySystem.Core.Models.Procedure;
using VeterinarySystem.Data;

namespace VeterinarySystem.Core.Services
{
	public class ProcedureService : IProcedureService
	{
		private readonly VeterinarySystemDbContext data;

		public ProcedureService(VeterinarySystemDbContext context)
		{
			data = context;
		}

		public Task CreateNewProcetude(ProcedureFormModel model)
		{
			throw new NotImplementedException();
		}

		public Task EditProcetude(ProcedureFormModel model)
		{
			throw new NotImplementedException();
		}

		public Task EditProcetude(int id)
		{
			throw new NotImplementedException();
		}
	}
}
