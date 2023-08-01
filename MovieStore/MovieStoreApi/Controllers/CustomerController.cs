using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MovieStoreApi.Dto;
using MovieStoreApi.Extensions;
using MovieStoreApi.Handlers.Customers.Commands;
using MovieStoreApi.Handlers.Customers.Queries;

namespace MovieStoreApi.Controllers;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCustomer.Response>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetCustomer.Query{ Id = id });
        return response.ToActionResult();
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<GetCustomers.Response>>> GetAll()
    {
        var response = await _mediator.Send(new GetCustomers.Query());
        return response.ToActionResult();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody]CustomerDto customerDto)
    {
        var response = await _mediator.Send(new AddCustomer.Command
        {
                Email = customerDto.Email
        });
        return response.ToCreatedResult();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteCustomer.Command{Id = id});
        return response.ToActionResult();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody]CustomerDto customerDto,Guid id)
    {
        var response = await _mediator.Send(new UpdateCustomer.Command
        {
                Email = customerDto.Email,
                Id = id
        });
        return response.ToActionResult();
    }
    
    [HttpPost("{customerId}/buy/movie/{movieId}/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PurchaseMovie(Guid customerId,Guid movieId)
    {
        var response = await _mediator.Send(new PurchaseMovie.Command
        {
                CustomerId = customerId,
                MovieId = movieId
        });
        return response.ToActionResult();
    }
    [HttpPatch("upgrade/advanced/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpgradeCustomer(Guid id)
    {
        var response = await _mediator.Send(new UpgradeCustomer.Command { Id = id });
        return response.ToActionResult();
    }
}