using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTOs.Auth;
using BSI.Integra.Aplicacion.Servicios.Service.Interfaz;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion
{
    public class AuthService : IAuthService
    {
        private readonly IDictionary<string, string> _tokens = new Dictionary<string, string>();
        public IDictionary<string, string> Tokens => _tokens;
        private IUnitOfWork _unitOfWork;
        private readonly string _key;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _key = configuration["Jwt:Key"]!;
            _unitOfWork = unitOfWork;
        }
        public ResultadoLoginDTO Login(UserCredentialDTO userCredential)
        {

            try
            {

                var datosToken = _unitOfWork.AuthRepository.ObtenerDatosToken(userCredential);
                if (datosToken == null)
                {
                    throw new UnauthorizedAccessRequestException("#AS-A-001@Usuario o clave incorrectos, vuelva a ingresar los datos correctamente.");
                }
                DateTime fechaExpiracion = DateTime.UtcNow.AddDays(1);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var encryptingCredentials = new EncryptingCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                    SecurityAlgorithms.Aes256CbcHmacSha512
                //SecurityAlgorithms.Aes128CbcHmacSha256
                );
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, datosToken.UserName),
                    new Claim(ClaimTypes.Role, datosToken.IdRol.ToString()),
                    new Claim("IdPersonal", datosToken.IdPersonal.ToString()),
                    new Claim("IdRol", datosToken.IdRol.ToString()),
                    new Claim("AreaTrabajo", datosToken.AreaTrabajo.ToString()),
                    new Claim("UserName", datosToken.UserName),
                    new Claim("UserAsp", datosToken.Id),
                    new Claim("Expira", fechaExpiracion.ToString()),
                    new Claim("TipoPersonal", datosToken.TipoPersonal.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    //Issuer = _issuer,
                    //Audience = _audience,
                    Expires = fechaExpiracion,
                    SigningCredentials = credentials,
                    //EncryptingCredentials = encryptingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var resultado = new ResultadoLoginDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    IdPersonal = datosToken.IdPersonal,
                    IdRol = datosToken.IdRol,
                    AreaTrabajo = datosToken.AreaTrabajo,
                    UserName = datosToken.UserName,
                    TipoPersonal = datosToken.TipoPersonal,
                };
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
