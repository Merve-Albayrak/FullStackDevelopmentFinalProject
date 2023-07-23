using Application.Features.Products.Commands.Add;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Features.Products.Queries.GetAll;

namespace WebAPI.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController:ApiControllerBase
    {

        [HttpPost("Create")]
        public async Task<IActionResult> AddAsync(ProductAddCommand productAddCommand)
        {


            return Ok(await Mediator.Send(productAddCommand));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync(ProductGetAllQuery getAllQuery)
        {


            return Ok(await Mediator.Send(getAllQuery));
        }
    }
}
