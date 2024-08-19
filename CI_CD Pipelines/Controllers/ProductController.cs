using CI_CD_Pipelines.Entities;
using CI_CD_Pipelines.Exceptions;
using CI_CD_Pipelines.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CI_CD_Pipelines.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public ActionResult<List<Product>> GetAllProducts()
    {
        return Ok(_productService.GetAllProducts());
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        Product? product = _productService.GetOneProduct(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with ID {id} not found." });
        }

        return Ok(product);
    }

    
    [HttpPost]
    public ActionResult<Product> AddProduct([FromBody] Product product)
    {
        if (product.Id != 0)
        {
            return BadRequest("When Adding A Product, Id should be 0");
        }
        Product addedProduct = _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
    }
    
    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest(new { Message = "Product ID mismatch." });
        }
        try
        {
            Product? updatedProduct = _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        var product = _productService.GetOneProduct(id);

        if (product == null)
        {
            return NotFound(new { Message = $"Product with ID {id} not found." });
        }
        
        _productService.DeleteProduct(product);
        return NoContent();
    }
    
    
}