using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.Exceptions;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpGet("top")]
    public IActionResult GetTop(int pageNumber = 1, int pageSize = 5, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (!startDate.HasValue) startDate = new DateTime(2015, 1, 1);
        if (!endDate.HasValue) endDate = new DateTime(2022, 12, 31);

        return Ok(_customerRepository.GetTop(pageNumber, pageSize, startDate.Value, endDate.Value));
    }

    [HttpPut("{customerId:int}")]
    public IActionResult UpdateCustomerName(int customerId, [FromBody] CustomerDto dto)
    {
        _logger.LogInformation($"Updating customer #{customerId} name...");

        try
        {
            _customerRepository.UpdateCustomerName(customerId, dto.Name);
        }
        catch (Exception ex)
        {
            if (ex is EntityNotFoundException)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return Ok();
    }
}