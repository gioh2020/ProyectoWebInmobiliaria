using netKubernetes.Dto.UsuarioDto;

namespace netKubernetes.Data.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<UsuarioResponseDto> getUsuario();
        Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request);
        Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDtocs request);
    }
}
