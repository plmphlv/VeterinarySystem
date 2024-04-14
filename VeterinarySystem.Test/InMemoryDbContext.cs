using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;

namespace VeterinarySystem.Test
{
	public class InMemoryDbContext
	{
		private readonly SqliteConnection connection;
		private readonly DbContextOptions<VeterinarySystemDbContext> options;

		public InMemoryDbContext()
		{
			connection = new SqliteConnection("FileName=:memory:");
			connection.Open();

			options = new DbContextOptionsBuilder<VeterinarySystemDbContext>()
				.UseSqlite(connection)
				.Options;

			using VeterinarySystemDbContext context = new VeterinarySystemDbContext(options);
			context.Database.EnsureCreated();
		}

		public VeterinarySystemDbContext CreateContext() => new VeterinarySystemDbContext(options);

		public async Task Dispose() => await connection.DisposeAsync();
	}
}