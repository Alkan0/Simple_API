using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xo_Test.Models;
using Xo_Test.Dtos;
using Xo_Test.Extensions;
using Xo_Test.Interfaces;

namespace Xo_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessesController : ControllerBase
    {
        private readonly Xo_TestContext _context;
        private readonly IBusinessRepository _repository;

        public BusinessesController(Xo_TestContext context, IBusinessRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpPost("AddBusiness")]
        public async Task<IActionResult> AddBusiness([FromBody] BusinessCreateDto businessDto)
        {
            if (businessDto == null)
            {
                return BadRequest("Business data is required.");
            }

            var createdBusiness = await _repository.AddBusinessAsync(businessDto);

            return CreatedAtAction(nameof(GetBusinessById), new { id = createdBusiness.BusinessId }, createdBusiness);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessById(int id)
        {
            var business = await _repository.GetBusinessByIdAsync(id);

            if (business == null)
            {
                return NotFound("Business not found.");
            }

            return Ok(business);
        }

        [HttpGet("ByName")]
        public async Task<IActionResult> GetBusinessesByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var businesses = await _repository.GetBusinessesByNameAsync(name);

            if (!businesses.Any())
            {
                return NotFound("No businesses found with the given name.");
            }

            return Ok(businesses);
        }

        [HttpGet("ByPhone")]
        public async Task<IActionResult> GetBusinessesByPhone([FromQuery] int phone)
        {
            if (phone == null)
            {
                return BadRequest("Phone parameter is required.");
            }

            var businesses = await _repository.GetBusinessesByPhoneAsync(phone);

            if (!businesses.Any())
            {
                return NotFound("No businesses found with the given phone.");
            }

            return Ok(businesses);
        }

        [HttpGet("ByZipCode")]
        public async Task<IActionResult> GetBusinessesByZipCode([FromQuery] string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                return BadRequest("ZipCode parameter is required.");
            }

            var businesses = await _repository.GetBusinessesByZipCodeAsync(zipCode);

            if (!businesses.Any())
            {
                return NotFound("No businesses found with the given ZipCode.");
            }

            return Ok(businesses);
        }

        [HttpPut("{id}/UpdateName")]
        public async Task<IActionResult> UpdateBusinessName(int id, [FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name is required.");
            }

            var updatedBusiness = await _repository.UpdateBusinessNameAsync(id, name);

            if (updatedBusiness == null)
            {
                return NotFound("Business not found.");
            }

            return Ok("Business name updated successfully.");
        }

        [HttpPut("{id}/UpdateAddress")]
        public async Task<IActionResult> UpdateBusinessAddress(int id, [FromBody] string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest("Address is required.");
            }

            var updatedBusiness = await _repository.UpdateBusinessAddressAsync(id, address);

            if (updatedBusiness == null)
            {
                return NotFound("Business not found.");
            }

            return Ok("Business address updated successfully.");
        }

        [HttpPut("{id}/UpdatePhones")]
        public async Task<IActionResult> UpdateBusinessPhones(int id, [FromBody] List<int> phones)
        {
            if (phones == null || !phones.Any())
            {
                return BadRequest("A list of phones is required.");
            }

            var result = await _repository.UpdateBusinessPhonesAsync(id, phones);

            if (!result)
            {
                return NotFound("Business not found.");
            }

            return Ok("Business phones updated successfully.");
        }

        [HttpPost("AddRelation")]
        public async Task<IActionResult> AddRelation([FromBody] BusinessRelationDto relationDto)
        {
            if (relationDto == null)
            {
                return BadRequest("Relation data is required.");
            }

            var result = await _repository.AddRelationAsync(relationDto);

            if (!result)
            {
                return NotFound("One or both businesses not found.");
            }

            return Ok("Relation added successfully.");
        }

        [HttpGet("{id}/Relations")]
        public async Task<IActionResult> GetBusinessRelations(int id)
        {
            var relations = await _repository.GetBusinessRelationsAsync(id);

            if (relations == null || !relations.Any())
            {
                return NotFound("No relations found for the given business.");
            }

            return Ok(relations);
        }

        [HttpPut("{id}/Activate")]
        public async Task<IActionResult> ActivateBusiness(int id)
        {
            var result = await _repository.ActivateBusinessAsync(id);

            if (!result)
            {
                return NotFound("Business not found.");
            }

            return Ok("Business activated successfully.");
        }

        [HttpPut("{id}/Deactivate")]
        public async Task<IActionResult> DeactivateBusiness(int id)
        {
            var result = await _repository.DeactivateBusinessAsync(id);

            if (!result)
            {
                return NotFound("Business not found.");
            }

            return Ok("Business deactivated successfully.");
        }

        [HttpPost("AddContract")]
        public async Task<IActionResult> AddContract([FromBody] ContractDto contractDto)
        {
            if (contractDto == null)
            {
                return BadRequest("Contract data is required.");
            }

            var result = await _repository.AddContractAsync(contractDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { Message = result.Message });
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is required.");
            }

            var (success, message, createdProduct) = await _repository.AddProductAsync(productDto);

            if (!success)
            {
                return BadRequest(message);
            }

            return CreatedAtAction(nameof(AddProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpGet("ActiveContracts")]
        public async Task<IActionResult> GetAllActiveContracts()
        {
            var contracts = await _repository.GetAllActiveContractsAsync();

            if (!contracts.Any())
            {
                return NotFound("No active contracts found.");
            }

            return Ok(contracts);
        }


        [HttpGet("ActiveContracts/{businessId}")]
        public async Task<IActionResult> GetActiveContractsByBusinessId(int businessId)
        {
            var contracts = await _repository.GetActiveContractsByBusinessIdAsync(businessId);

            if (!contracts.Any())
            {
                return NotFound($"No active contracts found for business with ID {businessId}.");
            }

            return Ok(contracts);
        }
    }
}
