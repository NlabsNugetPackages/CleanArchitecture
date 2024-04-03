using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;
public sealed class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator) {}

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Api çalışıyor");
    }
}
