using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;

namespace SIEET_API_REST.Controllers.v1;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
public class AlojamientoController : ControllerBase
{
    private readonly IAlojamientoService _AlojamientoService;
    private readonly IMapper _mapper;

    public AlojamientoController(IAlojamientoService AlojamientoService, IMapper mapper)
    {
        _AlojamientoService = AlojamientoService;
        _mapper = mapper;
    }

    [HttpGet("Alojamiento_Obten")]
    public async Task<IActionResult> AlojamientoObten()
    {
        try
        {
            var Lst_Alojamiento = await _AlojamientoService.Obten();

            return Ok(_mapper.Map<IEnumerable<DTO_Alojamiento_Obten>>(Lst_Alojamiento));
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}