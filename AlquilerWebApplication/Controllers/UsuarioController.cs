using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.Security.Claims;

namespace AlquilerWebApplication.Controllers;

//[Route("api/v{version:apiVersion}/[Controller]")]
[Route("api/[Controller]")]
//[ApiVersion("1")]
[ApiController]
[Authorize]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _UsuarioService;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioService UsuarioService, IMapper mapper)
    {
        _UsuarioService = UsuarioService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Obten()
    {
        try
        {
            var Lst_Usuario = await _UsuarioService.Obten();

            return Ok(_mapper.Map<IEnumerable<DTO_Usuario_Obten>>(Lst_Usuario));
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
    [HttpGet("Obten_Usuario_Logeado")]
    public async Task<IActionResult> Obten_Usuario_Logeado()
    {
        try
        {
            // 1. Usamos ?.Value para que si el Claim no existe, 'Email' sea simplemente null en lugar de tronar.
            var Email = User?.FindFirst(ClaimTypes.Email)?.Value;
            // 2. Validamos si el email es nulo o vacío antes de ir a la base de datos.
            if (string.IsNullOrEmpty(Email))
            {
                return Unauthorized(new DTO_Response<DTO_Usuario_Obten_x_Correo>
                {
                    IsSuccessful = false,
                });
            }

            var Usuario = await _UsuarioService.Obten_x_Correo(Email);

            if (Usuario is null)
                return NotFound(new DTO_Response<DTO_Usuario_Obten_x_Correo>
                {
                    IsSuccessful = false,
                });

            return Ok(new DTO_Response<DTO_Usuario_Obten_x_Correo>
            {
                IsSuccessful = true,
                Data = _mapper.Map<DTO_Usuario_Obten_x_Correo>(Usuario)
            });
        }
        catch (Exception ex)
        {
            // Loguear el error 'ex' aquí sería lo ideal
            return StatusCode(500, $"Error interno del servidor al procesar la solicitud: {ex}");
        }
    }
}