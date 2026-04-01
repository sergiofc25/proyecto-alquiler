using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AlquilerWebApplication.TokenServices;

namespace AlquilerWebApplication.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioService _UsuarioService;

    public TokenController(IUsuarioService UsuarioService, ITokenService tokenService)
    {
        _UsuarioService = UsuarioService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh([FromBody] DTO_RefreshToken eDTO_RefreshToken)
    {
        if (eDTO_RefreshToken is null)
            return BadRequest(new DTO_AuthResponse
            { IsAuthSuccessful = false, ErrorMessage = "Solicitud de cliente inválida" });

        var principal = _tokenService.GetPrincipalFromExpiredToken(eDTO_RefreshToken.Token);

        var Usuario =
            await _UsuarioService.Obten_Token_x_Correo(((ClaimsIdentity)principal.Identity).FindFirst(ClaimTypes.Email).Value);

        if (Usuario == null || Usuario.Usu_TokenActualizado != eDTO_RefreshToken.RefreshToken ||
            Usuario.Usu_FecHoraTokenActualizado <= DateTime.Now)
            return BadRequest(new DTO_AuthResponse
            { IsAuthSuccessful = false, ErrorMessage = "Solicitud de cliente inválida" });

        var signingCredentials = _tokenService.GetSigningCredentials();
        var claims = _tokenService.GetClaims(Usuario);
        var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var expirationTime = tokenOptions.ValidTo.ToLocalTime();

        await _UsuarioService.Actualiza_Token(Usuario);

        return Ok(new DTO_AuthResponse { Token = token, RefreshToken = Usuario.Usu_TokenActualizado, IsAuthSuccessful = true, Expires = expirationTime });
    }
}