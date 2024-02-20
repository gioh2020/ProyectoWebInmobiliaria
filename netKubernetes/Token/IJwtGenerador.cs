using netKubernetes.Models;

namespace netKubernetes.Token
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
