

using BSI.Integra.Aplicacion.DTOs.Auth;

namespace BSI.Integra.Repositorio.Repository.Auth.Interfaz
{
    public interface IAuthRepository
    {
        DatosTokenDTO? ObtenerDatosToken(UserCredentialDTO userCredential);
    }
}
