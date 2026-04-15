using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.IdentityModel.Tokens.Jwt;
using AlquilerWebApplication.TokenServices;
using AlquilerWebApplication.RequestFeatures;

namespace AlquilerWebApplication.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly IUsuarioService UsuarioService;
    private IMapper _mapper;

    public AccountsController(IConfiguration configuration, IUsuarioService UsuarioService, IMapper mapper, ITokenService tokenService)
    {
        this.UsuarioService = UsuarioService;
        _mapper = mapper;
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings");

        _tokenService = tokenService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] DTO_Usuario_Obten_Login eDTO_Usuario_Obten_Login)
    {
        var Usuario = await UsuarioService.Obten_x_Correo(eDTO_Usuario_Obten_Login.Email);

        if (Usuario == null) return BadRequest(new DTO_AuthResponse { ErrorMessage = "Usuario inválido." });

        var decryptedPassword = new CryptoConfiguration().Desencriptar(Usuario.Usu_Pass, _jwtSettings["securityKey"], Usuario.Usu_Salt);

        if (eDTO_Usuario_Obten_Login.Pass != decryptedPassword) return Unauthorized(new DTO_AuthResponse { ErrorMessage = "Autenticación no válida." });

        var signingCredentials = _tokenService.GetSigningCredentials();
        var claims = _tokenService.GetClaims(Usuario);
        var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        Usuario.Usu_Token = _tokenService.GenerateRefreshToken();
        Usuario.Usu_FechaHoraVencimientoToken = DateTime.Now.AddMinutes(double.Parse(_jwtSettings["expiryInMinutes"]));

        await UsuarioService.Actualiza_Token(Usuario);

        return Ok(new DTO_AuthResponse { IsAuthSuccessful = true, Token = token, RefreshToken = Usuario.Usu_Token });
    }

    [HttpGet("ServerTime")]
    [AllowAnonymous]
    public IActionResult GetServerTime()
    {
        return Ok(new
        {
            ServerTime = DateTime.UtcNow,
            TimeZone = TimeZoneInfo.Local.DisplayName,
            Message = "v13/24/2026"
        });
    }
}