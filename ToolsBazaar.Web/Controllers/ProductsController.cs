using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("most-expensive")]
    public IActionResult GetMostExpensiveProducts()
    {
        var products = _productRepository.GetAll()
            .OrderByDescending(p => p.Price)
            .ThenBy(p => p.Name)
            .ToList();

        return Ok(products);
    }
}