using Xo_Test.Dtos;
using Xo_Test.Models;

namespace Xo_Test.Extensions
{
    public static class BusinessExtensions
    {
        public static BusinessDto ToDto(this Business business)
        {
            return new BusinessDto
            {
                BusinessId = business.BusinessId,
                Name = business.Name,
                BrandName = business.BrandName,
                Address = business.Address,
                City = business.City,
                Coordinates = business.Coordinates,
                ZipCode = business.ZipCode,
                MainActivity = business.MainActivity,
                SecondaryActivities = business.SecondaryActivities?.Select(s => s.Name).ToList(),
                Phones = business.Phones?.Select(p => p.Number).ToList(),
                IsActive = business.IsActive
            };
        }

        public static Business ToEntity(this BusinessDto businessDto)
        {
            return new Business
            {
                BusinessId = businessDto.BusinessId,
                Name = businessDto.Name,
                BrandName = businessDto.BrandName,
                Address = businessDto.Address,
                City = businessDto.City,
                Coordinates = businessDto.Coordinates,
                ZipCode = businessDto.ZipCode,
                MainActivity = businessDto.MainActivity,
                SecondaryActivities = businessDto.SecondaryActivities?.Select(secondaryActivity => new SecondaryActivity { Name = secondaryActivity }).ToList(),
                Phones = businessDto.Phones?.Select(phone => new Phone { Number = phone }).ToList(),
                IsActive = businessDto.IsActive
            };
        }

        public static Business ToEntity(this BusinessCreateDto businessDto)
        {
            return new Business
            {
                Name = businessDto.Name,
                BrandName = businessDto.BrandName,
                Address = businessDto.Address,
                City = businessDto.City,
                Coordinates = businessDto.Coordinates,
                ZipCode = businessDto.ZipCode,
                MainActivity = businessDto.MainActivity,
                SecondaryActivities = businessDto.SecondaryActivities?.Select(secondaryActivity => new SecondaryActivity { Name = secondaryActivity }).ToList(),
                Phones = businessDto.Phones?.Select(phone => new Phone { Number = phone }).ToList()
            };
        }
    }
}
