using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entitie;
using Service;

namespace AlquilerWebApplication.Controllers;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
[Authorize]
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
    [HttpGet("Obten_x_Id/{Alo_Id}")]
    public async Task<IActionResult> Obten_x_Id(int Alo_Id)
    {
        try
        {
            var Alojamiento = await _AlojamientoService.Obten_x_Id(Alo_Id);

            if (Alojamiento is null)
                return NotFound();
            return Ok(new DTO_Response<DTO_Alojamiento_Obten_x_Id> { IsSuccessful = true, Data = _mapper.Map<DTO_Alojamiento_Obten_x_Id>(Alojamiento) });

        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpPut("Actualiza/{Alo_Id}")]
    public async Task<IActionResult> Actualiza(int Alo_Id, [FromBody] DTO_Alojamiento_Actualiza eDTO_Alojamiento_Actualiza)
    {
        try
        {
            // Validación de datos nulos
            if (eDTO_Alojamiento_Actualiza is null)
                return BadRequest(new DTO_Response<object> { ErrorMessage = "Datos nulos." });

            // Verificar existencia del Alojamiento
            var AlojamientoExiste = await _AlojamientoService.Obten_x_Id(Alo_Id);
            if (AlojamientoExiste is null)
                return BadRequest(new DTO_Response<object> { ErrorMessage = "Datos inexistentes." });

            // Mapear y actualizar
            _mapper.Map(eDTO_Alojamiento_Actualiza, AlojamientoExiste);
            var mensajeError = await _AlojamientoService.Actualiza(AlojamientoExiste);

            // Manejar respuesta
            if (mensajeError == string.Empty)
                return Ok(new DTO_Response<object>
                {
                    IsSuccessful = true
                });

            return BadRequest(new DTO_Response<object> { ErrorMessage = mensajeError });
        }
        catch (Exception)
        {
            return StatusCode(500, new DTO_Response<object> { ErrorMessage = "Error interno del servidor." });
        }
    }
}