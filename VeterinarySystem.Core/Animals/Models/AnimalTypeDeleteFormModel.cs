﻿namespace Animal.Animal
{
    public class AnimalTypeDeleteFormModel
    {
        public int TypeId { get; set; }

        public ICollection<AnimalTypesServiceModels> AnimalTypes { get; set; } = new List<AnimalTypesServiceModels>();
    }
}