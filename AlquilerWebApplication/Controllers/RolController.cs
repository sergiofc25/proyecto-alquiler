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

public class RolController : ControllerBase
{
    private readonly IRolService _RolService;
    private readonly IMapper _mapper;

    public RolController(IRolService RolService, IMapper mapper)
    {
        _RolService = RolService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Obten()
    {
        try
        {
            var Lst_Rol = await _RolService.Obten();

            return Ok(_mapper.Map<IEnumerable<DTO_Rol>>(Lst_Rol));
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}