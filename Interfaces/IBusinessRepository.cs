using Xo_Test.Dtos;
using Xo_Test.Models;

namespace Xo_Test.Interfaces
{
    public interface IBusinessRepository
    {
        Task<List<BusinessDto>> GetBusinessesByZipCodeAsync(string zipCode);
        Task<List<BusinessDto>> GetBusinessesByNameAsync(string name);
        Task<List<BusinessDto>> GetBusinessesByPhoneAsync(int phone);
        Task<BusinessDto> AddBusinessAsync(BusinessCreateDto businessDto);
        Task<BusinessDto?> GetBusinessByIdAsync(int id);
        Task<bool> UpdateBusinessNameAsync(int id, string name);
        Task<bool> UpdateBusinessAddressAsync(int id, string address);
        Task<bool> UpdateBusinessPhoneAsync(int id, int phone);
        Task<bool> UpdateBusinessPhonesAsync(int id, List<int> phones);
        Task<List<BusinessRelationDto>> GetBusinessRelationsAsync(int businessId);
        Task<bool> ActivateBusinessAsync(int id);
        Task<bool> AddRelationAsync(BusinessRelationDto relationDto);
        Task<bool> DeactivateBusinessAsync(int id);
        Task<(bool Success, string Message)> AddContractAsync(ContractDto contractDto);
        Task<(bool Success, string Message, Product? CreatedProduct)> AddProductAsync(ProductDto productDto);
        Task<List<ContractDto>> GetAllActiveContractsAsync();
        Task<List<ContractDto>> GetActiveContractsByBusinessIdAsync(int businessId);
    }
}
