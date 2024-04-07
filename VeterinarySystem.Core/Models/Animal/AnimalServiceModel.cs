﻿namespace VeterinarySystem.Core.Models.Animal
{
    public class AnimalServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public double Weight { get; set; }

        public string AnimalTypeName { get; set; } = null!;

		public string OwnerFullName { get; set; } = null!;
	}
}