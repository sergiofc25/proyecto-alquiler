using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model;
using Service;
using System.Text.Json;
using Model.Entitie;

namespace AlquilerWebApplication.Controllers;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
[Authorize]

public class ContratoController : ControllerBase
{
    private readonly IContratoService _ContratoService;
    private readonly IMapper _mapper;

    public ContratoController(IContratoService ContratoService, IMapper mapper)
    {
        _ContratoService = ContratoService;
        _mapper = mapper;
    }

    [HttpGet("Obten_Paginado/{RegistroPagina}/{NumeroPagina}")]
    public async Task<IActionResult> Obten_Paginado(int RegistroPagina, int NumeroPagina, [FromQuery] string? TerBusqueda = null)
    {
        try
        {
            TerBusqueda ??= string.Empty;
            (int TotalPagina, int TotalRegistro, bool TienePaginaAnterior, bool TienePaginaProximo, var Lst_Contrato) = await _ContratoService.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);

            var metadata = new
            {
                RegistroPagina,
                NumeroPagina,
                TotalPagina,
                TotalRegistro,
                TienePaginaAnterior,
                TienePaginaProximo
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(new DTO_ResponsePag<IEnumerable<DTO_Contrato_Obten_Paginado>>
            {
                PaginaActual = NumeroPagina,
                TotalDePagina = TotalPagina,
                ElementosPorPagina = RegistroPagina,
                TotalDeElementos = TotalRegistro,
                IsSuccessful = true,
                Data = _mapper.Map<IEnumerable<DTO_Contrato_Obten_Paginado>>(Lst_Contrato)
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpPost("Crea")]
    public async Task<IActionResult> Crea([FromBody] DTO_Contrato_Crea eDTO_Contrato_Crea)
    {
        try
        {
            if (eDTO_Contrato_Crea == null)
            {
                return BadRequest(new DTO_Response<object>
                {
                    IsSuccessful = false,
                    ErrorMessage = "Los datos del contrato no pueden ser nulos."
                });
            }

            // Mapear DTO a entidad
            var contrato = _mapper.Map<Ent_Contrato>(eDTO_Contrato_Crea);

            // Crear el contrato
            var (Con_Id, mensajeError) = await _ContratoService.Crea(contrato);

            if (!string.IsNullOrEmpty(mensajeError))
            {
                return BadRequest(new DTO_Response<object>
                {
                    IsSuccessful = false,
                    ErrorMessage = mensajeError
                });
            }

            // Obtener el contrato completo recién creado
            var contratoCompleto = await _ContratoService.Obten_x_Id(Con_Id);

            if (contratoCompleto == null)
            {
                return StatusCode(500, new DTO_Response<object>
                {
                    IsSuccessful = false,
                    ErrorMessage = "El contrato se creó pero no se pudo recuperar la información completa."
                });
            }

            // Mapear a DTO de respuesta
            var contratoCreadoDTO = _mapper.Map<DTO_Contrato_Obten_x_Id>(contratoCompleto);

            return Ok(new DTO_Response<DTO_Contrato_Obten_x_Id>
            {
                Data = contratoCreadoDTO,
                IsSuccessful = true,
                ErrorMessage = "Contrato creado exitosamente."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new DTO_Response<object>
            {
                IsSuccessful = false,
                ErrorMessage = $"Error interno del servidor: {ex.Message}"
            });
        }
    }
    [HttpGet("Obten_x_Id/{Con_Id}")]
    public async Task<IActionResult> Obten_x_Id(int Con_Id)
    {
        try
        {
            var Contrato = await _ContratoService.Obten_x_Id(Con_Id);

            if (Contrato is null)
                return NotFound();
            return Ok(new DTO_Response<DTO_Contrato_Obten_x_Id> { IsSuccessful = true, Data = _mapper.Map<DTO_Contrato_Obten_x_Id>(Contrato) });

        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpPut("Actualiza_Cerrar/{Con_Id}")]
    public async Task<IActionResult> Actualiza_Cerrar(int Con_Id)
    {
        try
        {
            var mensaje = await _ContratoService.Actualiza_Cerrar(Con_Id);

            if (!string.IsNullOrEmpty(mensaje))
            {
                return BadRequest(new DTO_Response<object>
                {
                    ErrorMessage = mensaje
                });
            }

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, new DTO_Response<object>
            {
                ErrorMessage = "Error interno del servidor."
            });
        }
    }
}