using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Extensions;
using MovieStoreApi.Handlers.Movies.Commands;
using MovieStoreApi.Handlers.Movies.Queries;
using MovieStoreApi.Movies.Commands;

namespace MovieStoreApi.Controllers;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly ISender _mediator;

    public MovieController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetMovie.Response>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetMovie.Query{ Id = id });
        return response.ToActionResult();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody]MovieDto movieDto)
    {
        var response = await _mediator.Send(new AddMovie.Command
        {
                Name = movieDto.Name,
                LicensingType = movieDto.LicensingType
        });
        return response.ToCreatedResult();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetMovies.Response>>> GetAll()
    {
        var response = await _mediator.Send(new GetMovies.Query());
        return response.ToActionResult();
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody]MovieDto movieDto,Guid id)
    {
        var response = await _mediator.Send(new UpdateMovie.Command
        {
                Id = id,
                Name = movieDto.Name,
                LicensingType = movieDto.LicensingType
        });
        return response.ToActionResult();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteMovie.Command{ Id = id });
        return response.ToActionResult();
    }
}