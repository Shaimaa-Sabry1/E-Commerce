using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager):ControllerBase
    {
        //endpoint :public non-static method

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery]ProductSpecificationsParamters product)
        {
          var result=await  serviceManager.ProductService.GetAllProductAsync(product);
            if (result is null) return BadRequest();
            return Ok(result);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductService.GettProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrand()
        {
            var result = await serviceManager.ProductService.GetAllBrandAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GatAllType()
        {
            var result = await serviceManager.ProductService.GetAllTypeAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
