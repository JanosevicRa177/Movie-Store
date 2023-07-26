using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Extensions;
using MovieStoreApi.Handlers.Movies.Commands;
using MovieStoreApi.Handlers.Movies.Queries;
using MovieStoreApi.Movies.Commands;
using MovieStoreApi.Movies.Queries;

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
    public async Task<ActionResult<Movie>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetMovie.Query{ Id = id });
        return response.ToActionResult();
    }
    [HttpPost]
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
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var response = await _mediator.Send(new GetMovies.Query());
        return response.ToActionResult();
    }
    [HttpPut("{id}")]
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
    [HttpGet("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteMovie.Command{ Id = id });
        return response.ToActionResult();
    }
}