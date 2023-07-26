using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<GetCustomer.Response>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetCustomer.Query{ Id = id });
        return response.ToActionResult();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCustomers.Response>>> GetAll()
    {
        var response = await _mediator.Send(new GetCustomers.Query());
        return response.ToActionResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]CustomerDto customerDto)
    {
        var response = await _mediator.Send(new AddCustomer.Command
        {
                Email = customerDto.Email,
                Name = customerDto.Name
        });
        return response.ToCreatedResult();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteCustomer.Command{Id = id});
        return response.ToActionResult();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody]CustomerDto customerDto,Guid id)
    {
        var response = await _mediator.Send(new UpdateCustomer.Command
        {
                Email = customerDto.Email,
                Name = customerDto.Name,
                Id = id
        });
        return response.ToActionResult();
    }
    
    [HttpPost("{customerId}/buy/movie/{movieId}/")]
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
    public async Task<IActionResult> UpgradeCustomer(Guid id)
    {
        var response = await _mediator.Send(new UpgradeCustomer.Command { Id = id });
        return response.ToActionResult();
    }
}