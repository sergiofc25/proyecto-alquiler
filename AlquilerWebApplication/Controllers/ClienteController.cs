using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.Text.Json;

namespace SIEET_API_REST.Controllers.v1;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _ClienteService;
    private readonly IMapper _mapper;

    public ClienteController(IClienteService ClienteService, IMapper mapper)
    {
        _ClienteService = ClienteService;
        _mapper = mapper;
    }

    [HttpGet("Obten_Paginado/{RegistroPagina}/{NumeroPagina}")]
    public async Task<IActionResult> Obten_Paginado(int RegistroPagina, int NumeroPagina, [FromQuery] string? TerBusqueda = null)
    {
        try
        {
            TerBusqueda ??= string.Empty;
            (int TotalPagina, int TotalRegistro, bool TienePaginaAnterior, bool TienePaginaProximo, var Lst_Cliente) = await _ClienteService.Obten_Paginado(RegistroPagina, NumeroPagina, TerBusqueda);

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

            return Ok(new DTO_ResponsePag<IEnumerable<DTO_Cliente_Obten_Paginado>>
            {
                PaginaActual = NumeroPagina,
                TotalDePagina = TotalPagina,
                ElementosPorPagina = RegistroPagina,
                TotalDeElementos = TotalRegistro,
                IsSuccessful = true,
                Data = _mapper.Map<IEnumerable<DTO_Cliente_Obten_Paginado>>(Lst_Cliente)
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}