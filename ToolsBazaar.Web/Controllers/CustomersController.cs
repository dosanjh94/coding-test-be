using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.Services;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerSpendingService _customerSpendingService;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerSpendingService customerSpendingService, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerSpendingService = customerSpendingService;
        _customerRepository = customerRepository;
    }

    //Low on time - Should unit test for try catch logic
    //Turned this into a patch as it is a partial upadate
    [HttpPatch("{customerId:int}")]
    public IActionResult UpdateCustomerName(int customerId, [FromBody] CustomerDto dto)
    {
        _logger.LogInformation($"Updating customer #{customerId} name...");

        try
        {
            _customerRepository.UpdateCustomerName(customerId, dto.Name);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning($"Customer #{customerId} not found");

            return NotFound($"Customer #{customerId} not found");
        }
    }

    [HttpGet("top")]
    public IActionResult GetTopSpendingCustomers()
    {
        var result = _customerSpendingService.GetTopCustomersBySpending();
        return Ok(result);
    }
}