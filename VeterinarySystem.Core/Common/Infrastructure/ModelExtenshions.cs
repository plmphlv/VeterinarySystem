using Animal.Contracts;
using AnimalOwner.Contracts;
using Common.Contracts;

namespace Common.Infrastructure
{
    public static class ModelExtenshions
    {
        public static string GetInformationWithDescription(this IDescription modelWithDescription)
        {
            string descriptionInfo = modelWithDescription.Description.Replace(" ", "-");

            return descriptionInfo;
        }

        public static string GetOwnerInformation(this IOwner owner)
        {
            string ownerInfo = owner.FullName.Replace(" ", "-");

            return ownerInfo;
        }
        public static string GetAnimalInformation(this IAnimal animal)
        {
            string animalInfo = animal.AnimalName.Replace(" ", "-");

            return animalInfo;
        }
    }
}
