using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase 
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet("most-expensive")]
    public IActionResult GetMostExpensive()
    {
        _logger.LogInformation("Getting most expensive products...");

        return Ok(_productRepository.GetMostExpensive());
    }
}