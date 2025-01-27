using Microsoft.EntityFrameworkCore;
using Xo_Test.Dtos;
using Xo_Test.Extensions;
using Xo_Test.Interfaces;
using Xo_Test.Models;

namespace Xo_Test.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly Xo_TestContext _context;

        public BusinessRepository(Xo_TestContext context)
        {
            _context = context;
        }

        public async Task<List<BusinessDto>> GetBusinessesByZipCodeAsync(string zipCode)
        {
            var businesses = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .Where(b => b.ZipCode == zipCode)
                .ToListAsync();

            return businesses.Select(b => b.ToDto()).ToList();
        }

        public async Task<List<BusinessDto>> GetBusinessesByNameAsync(string name)
        {
            var businesses = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .Where(b => b.Name.Contains(name))
                .ToListAsync();

            return businesses.Select(b => b.ToDto()).ToList();
        }

        public async Task<BusinessDto> AddBusinessAsync(BusinessCreateDto businessDto)
        {
            var business = businessDto.ToEntity();

            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            return business.ToDto();
        }

        public async Task<BusinessDto?> GetBusinessByIdAsync(int id)
        {
            var business = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .FirstOrDefaultAsync(b => b.BusinessId == id);

            return business?.ToDto();
        }

        public async Task<List<BusinessDto>> GetBusinessesByPhoneAsync(int phone)
        {
            var businesses = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .Where(b => b.Phones.Any(p => p.Number == phone))
                .ToListAsync();

            return businesses.Select(b => b.ToDto()).ToList();
        }

        public async Task<bool> AddRelationAsync(BusinessRelationDto relationDto)
        {
            var business1 = await _context.Businesses.FindAsync(relationDto.BusinessId1);
            var business2 = await _context.Businesses.FindAsync(relationDto.BusinessId2);

            if (business1 == null || business2 == null)
            {
                return false; 
            }

            var relation = new BusinessRelation
            {
                BusinessId1 = relationDto.BusinessId1,
                BusinessId2 = relationDto.BusinessId2,
                RelationType = relationDto.RelationType
            };

            _context.BusinessRelations.Add(relation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBusinessNameAsync(int id, string name)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return false;
            }

            business.Name = name;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBusinessAddressAsync(int id, string address)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return false;
            }

            business.Address = address;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBusinessPhoneAsync(int id, int phone)
        {
            var business = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .FirstOrDefaultAsync(b => b.BusinessId == id);

            if (business == null)
            {
                return false;
            }

            var phoneEntity = business.Phones.FirstOrDefault();
            if (phoneEntity != null)
            {
                phoneEntity.Number = phone;
            }
            else
            {
                business.Phones.Add(new Phone { Number = phone });
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBusinessPhonesAsync(int id, List<int> phones)
        {
            var business = await _context.Businesses
                .Include(b => b.Phones)
                .Include(b => b.SecondaryActivities)
                .FirstOrDefaultAsync(b => b.BusinessId == id);

            if (business == null)
            {
                return false;
            }

            _context.Phones.RemoveRange(business.Phones);

            business.Phones = phones.Select(phone => new Phone { Number = phone }).ToList();

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BusinessRelationDto>> GetBusinessRelationsAsync(int businessId)
        {
            var relations = await _context.BusinessRelations
                .Where(br => br.BusinessId1 == businessId || br.BusinessId2 == businessId)
                .Select(br => new BusinessRelationDto
                {
                    BusinessId1 = br.BusinessId1,
                    BusinessId2 = br.BusinessId2,
                    RelationType = br.RelationType
                })
                .ToListAsync();

            return relations;
        }

        public async Task<bool> ActivateBusinessAsync(int id)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return false;
            }

            business.IsActive = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateBusinessAsync(int id)
        {
            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return false;
            }

            business.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(bool Success, string Message)> AddContractAsync(ContractDto contractDto)
        {
            var business = await _context.Businesses
                .Include(b => b.SecondaryActivities)
                .FirstOrDefaultAsync(b => b.BusinessId == contractDto.BusinessId);

            if (business == null)
            {
                return (false, "Business not found.");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == contractDto.ProductId);

            if (product == null)
            {
                return (false, "Product not found.");
            }

            var isActivityAllowed = product.Activity == business.MainActivity ||
                                    business.SecondaryActivities.Any(sa => sa.Name == product.Activity);

            if (!isActivityAllowed)
            {
                return (false, "Business is not allowed to purchase products in this activity.");
            }

            var contract = new Contract
            {
                BusinessId = contractDto.BusinessId,
                ProductId = contractDto.ProductId,
                EndDate = contractDto.EndDate
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return (true, "Contract created successfully.");
        }

        public async Task<(bool Success, string Message, Product? CreatedProduct)> AddProductAsync(ProductDto productDto)
        {
            // Έλεγχος αν υπάρχει προϊόν με τον ίδιο κωδικό
            if (await _context.Products.AnyAsync(p => p.Code == productDto.Code))
            {
                return (false, "A product with the same code already exists.", null);
            }

            // Δημιουργία του νέου προϊόντος
            var product = new Product
            {
                Code = productDto.Code,
                Name = productDto.Name,
                Activity = productDto.Activity,
                Price = productDto.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Επιστροφή επιτυχίας και του δημιουργημένου προϊόντος
            return (true, "Product added successfully.", product);
        }

        public async Task<List<ContractDto>> GetAllActiveContractsAsync()
        {
            var activeContracts = await _context.Contracts
                .Include(c => c.Business)
                .Include(c => c.Product)
                .Where(c => c.EndDate >= DateTime.UtcNow)
                .Select(c => new ContractDto
                {
                    BusinessId = c.BusinessId,
                    ProductId = c.ProductId,
                    EndDate = c.EndDate
                })
                .ToListAsync();

            return activeContracts;
        }

        public async Task<List<ContractDto>> GetActiveContractsByBusinessIdAsync(int businessId)
        {
            var activeContracts = await _context.Contracts
                .Include(c => c.Product)
                .Where(c => c.BusinessId == businessId)
                .Select(c => new ContractDto
                {
                    BusinessId = c.BusinessId,
                    ProductId = c.ProductId,
                    EndDate = c.EndDate
                })
                .ToListAsync();

            return activeContracts;
        }
    }
}