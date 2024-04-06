using Microsoft.AspNetCore.Identity;
using VeterinarySystem.Data.Domain.Entities;

namespace VeterinarySystem.Data.Domain.DataSeed
{
    internal class DataSeed
    {
        public StaffMember Administrator { get; set; }

        public StaffMember Vet { get; set; }

        //public AnimalOwner Owner { get; set; }

        //public Animal Pet1 { get; set; }

        //public Animal Pet2 { get; set; }
        public DataSeed()
        {
            SeedUsers();
        }

        private void SeedUsers()
        {
            PasswordHasher<StaffMember> hasher = new PasswordHasher<StaffMember>();

            Administrator = new StaffMember()
            {
                Id = "bcb4f420-ch4d-g1ga-ab26-c069c6f364e",
                UserName = "chad@admin.com",
                NormalizedUserName = "CHAD@ADMIM.COM",
                Email = "chad@admin.com",
                NormalizedEmail = "CHAD@ADMIN.COM",
                FirstName = "Гига",
                LastName = "Чад",
                PasswordHash = hasher.HashPassword(Administrator, "DealWithIt!")
            };

            Vet = new StaffMember()
            {
                Id = "d49db8c9-73e5-4e24-8cb3-80b9ee257ba3",
                UserName = "steli@vet.com",
                NormalizedUserName = "STELI@VET.COM",
                Email = "steli@vet.com",
                NormalizedEmail = "STELI@VET.COM",
                FirstName = "Стелияна",
                LastName = "Трифонова",
                PasswordHash = hasher.HashPassword(Vet, "Steli123")
            };

        }
    }
}
