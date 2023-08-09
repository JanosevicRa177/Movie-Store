using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Extensions;
using MovieStoreApi.Handlers.Movies.Commands;
using MovieStoreApi.Handlers.Movies.Queries;

namespace MovieStoreApi.Controllers;

[ApiController]
[Authorize]
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
    public async Task<IActionResult> Add([FromBody]CreateMovieDto createMovieDto)
    {
        var response = await _mediator.Send(new AddMovie.Command
        {
                Name = createMovieDto.Name,
                LicensingType = createMovieDto.LicensingType
        });
        return response.ToCreatedResult();
    }
    [HttpGet]
    public async Task<ActionResult<GetMovies.Response>> GetAll([FromQuery] MoviePaginationDto moviePaginationDto)
    {
        var response = await _mediator.Send(new GetMovies.Query{MoviePaginationDto = moviePaginationDto});
        return response.ToActionResult();
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody]CreateMovieDto createMovieDto,Guid id)
    {
        var response = await _mediator.Send(new UpdateMovie.Command
        {
                Id = id,
                Name = createMovieDto.Name,
                LicensingType = createMovieDto.LicensingType
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