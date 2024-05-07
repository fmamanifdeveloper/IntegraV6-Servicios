
using BSI.Integra.Aplicacion.DTOs.Auth;

namespace BSI.Integra.Aplicacion.Servicios.Service.Interfaz
{
    public interface IAuthService
    {
        ResultadoLoginDTO Login(UserCredentialDTO userCredential);
    }
}
