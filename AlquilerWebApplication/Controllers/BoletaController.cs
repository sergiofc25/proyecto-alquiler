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
public class BoletaController : ControllerBase
{
    private readonly IBoletaService _BoletaService;
    private readonly IMapper _mapper;

    public BoletaController(IBoletaService BoletaService, IMapper mapper)
    {
        _BoletaService = BoletaService;
        _mapper = mapper;
    }

    [HttpGet("Obten_x_Pago/{Pag_Id}")]
    public async Task<IActionResult> Obten_x_Pago(int Pag_Id)
    {
        try
        {
            var Boleta = await _BoletaService.Obten_x_Pago(Pag_Id);

            if (Boleta is null)
                return NotFound();
            return Ok(new DTO_Response<DTO_Boleta_Obten_x_Pago> { IsSuccessful = true, Data = _mapper.Map<DTO_Boleta_Obten_x_Pago>(Boleta) });

        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}