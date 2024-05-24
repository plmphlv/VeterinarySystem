﻿using Animal.Contracts;
using Common.Contracts;

namespace Procedures.Procedure
{
    public class ProcedureServiceModel : IDescription, IAnimal
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public int AnimalId { get; set; }

        public string AnimalName { get; set; } = string.Empty;

        public string StaffMemberFullName { get; set; } = string.Empty;
    }
}