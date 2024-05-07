using BSI.Integra.Aplicacion.DTOs.Auth;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Interfaz;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _authService = new AuthService(configuration, unitOfWork);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserCredentialDTO userCredential)
        {
            var resultado = _authService.Login(userCredential);
            return Ok(resultado);
        }
    }
}
