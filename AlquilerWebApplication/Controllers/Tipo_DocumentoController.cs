using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;

namespace AlquilerWebApplication.Controllers;

//[Route("api/v{version:apiVersion}/[Controller]")]
//[ApiVersion("1")]
//[ApiController]
[Route("api/[Controller]")]
[ApiController]
[Authorize]

public class Tipo_DocumentoController : ControllerBase
{
    private readonly ITipo_DocumentoService _Tipo_DocumentoService;
    private readonly IMapper _mapper;

    public Tipo_DocumentoController(ITipo_DocumentoService Tipo_DocumentoService, IMapper mapper)
    {
        _Tipo_DocumentoService = Tipo_DocumentoService;
        _mapper = mapper;
    }

    //[HttpGet("Obten")]
    //public async Task<IActionResult> Obten()
    //{
    //    try
    //    {
    //        var Lst_Tipo_Documento = await _Tipo_DocumentoService.Obten();

    //        return Ok(_mapper.Map<IEnumerable<DTO_Tipo_Documento_Obten>>(Lst_Tipo_Documento));
    //    }
    //    catch (Exception)
    //    {
    //        return StatusCode(500, "Error interno del servidor.");
    //    }
    //}
    [HttpGet("Obten")]
    public async Task<IActionResult> Obten()
    {
        try
        {
            var Lst_Tipo_Documento = await _Tipo_DocumentoService.Obten();

            if (Lst_Tipo_Documento is null) return NotFound(new DTO_Response<object> { ErrorMessage = "Datos no encontrados." });

            return Ok(new DTO_Response<List<DTO_Tipo_Documento_Obten>> { IsSuccessful = true, Data = _mapper.Map<List<DTO_Tipo_Documento_Obten>>(Lst_Tipo_Documento) });
        }
        catch (Exception)
        {
            return StatusCode(500, new DTO_Response<object> { ErrorMessage = "Error interno del servidor." });
        }
    }
}