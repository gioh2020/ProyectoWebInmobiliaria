using Microsoft.AspNetCore.Identity;
using netKubernetes.Dto.UsuarioDto;
using netKubernetes.Models;
using netKubernetes.Token;

namespace netKubernetes.Data.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSesion _usuarioSesion;

        public UsuarioRepository(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IJwtGenerador jwtGenerador,
            AppDbContext contexto,
            UsuarioSesion usuarioSesion     
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerador = jwtGenerador;
            _contexto = contexto;
            _usuarioSesion = usuarioSesion;           
        }

        private UsuarioResponseDto TransformerToUserToUserDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _jwtGenerador.CrearToken(usuario)
            };
        }

        public async Task<UsuarioResponseDto> getUsuario()
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
            return TransformerToUserToUserDto(usuario!);
        }

      

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);
            return TransformerToUserToUserDto(usuario!);
        }

        public async Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDtocs request)
        {
            var usuario = new Usuario {
         
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Email = request.Email,
                UserName = request.UserName,    

            };

            await  _userManager.CreateAsync(usuario, request.Password!);
            return TransformerToUserToUserDto(usuario);
        }
    }
}
