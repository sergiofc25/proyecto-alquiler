using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.Text.Json;

namespace AlquilerWebApplication.Controllers;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
//[Authorize]

public class PagoController : ControllerBase
{
    private readonly IPagoService _PagoService;
    private readonly IMapper _mapper;

    public PagoController(IPagoService PagoService, IMapper mapper)
    {
        _PagoService = PagoService;
        _mapper = mapper;
    }

    [HttpGet("Obten_Paginado/{RegistroPagina}/{NumeroPagina}")]
    public async Task<IActionResult> Obten_Paginado(int RegistroPagina, int NumeroPagina, [FromQuery] string? TerBusqueda = null)
    {
        try
        {
            TerBusqueda ??= string.Empty;
            (int TotalPagina, int TotalRegistro, bool TienePaginaAnterior, bool TienePaginaProximo, var Lst_Pago) = await _PagoService.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);

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

            return Ok(new DTO_ResponsePag<IEnumerable<DTO_Pago_Obten_Paginado>>
            {
                PaginaActual = NumeroPagina,
                TotalDePagina = TotalPagina,
                ElementosPorPagina = RegistroPagina,
                TotalDeElementos = TotalRegistro,
                IsSuccessful = true,
                Data = _mapper.Map<IEnumerable<DTO_Pago_Obten_Paginado>>(Lst_Pago)
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpGet("Obten_Paginado_Contrato/{RegistroPagina}/{NumeroPagina}/{Con_Id}")]
    public async Task<IActionResult> Obten_Paginado_x_Contrato(int RegistroPagina, int NumeroPagina, string Con_Id, [FromQuery] string? TerBusqueda = null)
    {
        try
        {
            TerBusqueda ??= string.Empty;
            (int TotalPagina, int TotalRegistro, bool TienePaginaAnterior, bool TienePaginaProximo, var Lst_Pago) = await _PagoService.Obten_Paginado_x_Contrato(RegistroPagina, NumeroPagina, TerBusqueda,Con_Id);

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

            return Ok(new DTO_ResponsePag<IEnumerable<DTO_Pago_Obten_Paginado>>
            {
                PaginaActual = NumeroPagina,
                TotalDePagina = TotalPagina,
                ElementosPorPagina = RegistroPagina,
                TotalDeElementos = TotalRegistro,
                IsSuccessful = true,
                Data = _mapper.Map<IEnumerable<DTO_Pago_Obten_Paginado>>(Lst_Pago)
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpPut("Actualiza_Pagar/{Pag_Id}/{FechaPagoRealizado}")]
    public async Task<IActionResult> Actualiza_Pagar(int Pag_Id, DateOnly FechaPagoRealizado)
    {
        try
        {
            var mensaje = await _PagoService.Actualiza_Pagar(Pag_Id, FechaPagoRealizado);

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