using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoZone.Core.DTOs;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;
using PhotoZone.ViewModels;

namespace PhotoZone.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlaceService _placeService;

    public PlaceController(IMapper mapper, IPlaceService placeService)
    {
        _mapper = mapper;
        _placeService = placeService;
    }

    [HttpGet("[action]")]
    public IActionResult GetAllPlaces()
    {
        List<PlaceDto> res = _placeService.GetAllPlaces();

        return Ok(_mapper.Map<List<PlaceDto>,List<PlaceViewModel>>(res));
    }

    [HttpGet("[action]/{id}")]
    public IActionResult GetPlaceById(Guid id)
    {
        try
        {
            return Ok(_placeService.GetPlace(id));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewPlace([FromBody] PlaceViewModel placeViewModel)
    {

        try
        {
            return Ok(_placeService.AddNewPlace(_mapper.Map<PlaceViewModel, PlaceDto>(placeViewModel)));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("[action]/{id}")]
    public IActionResult GetAllPlacesByUserId(Guid id)
    {
        try
        {
            return Ok(_placeService.GetAllPlacesByUserId(id));
        }
        catch (PhotoZoneException e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpPost("[action]")]
    public IActionResult SearchPlaces(SearchViewModel searchViewModel)
    {
        return Ok(_placeService.SearchPlaces(searchViewModel.searchText));
    }

    [HttpDelete("[action]/{id}")]
    public IActionResult DeletePlace(Guid id)
    {
        return Ok();
    }
}