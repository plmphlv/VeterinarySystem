namespace Procedures.Procedure
{
    public class ProcedureQueryServiceModel
    {
        public int TotalProcedures { get; set; }

        public int TotalPages { get; set; }

        public ICollection<ProcedureServiceModel> Procedures { get; set; } = new List<ProcedureServiceModel>();
    }
}
