using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using netKubernetes.Dto.UsuarioDto;
using netKubernetes.Middleware;
using netKubernetes.Models;
using netKubernetes.Token;
using System.Net;

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

        public async Task<UsuarioResponseDto> GetUsuario()
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
            if (usuario is null)
            {
                throw new MiddlewareException(HttpStatusCode.Unauthorized, new {mensaje = "El usuario no existe en la base de datos"});
            }

            return TransformerToUserToUserDto(usuario!);
        }

      

        public async Task<UsuarioResponseDto> Login(UsuarioLoginRequestDto request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);

            if (usuario is null)
            {
                throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensaje = "El email del usuario no existe en la base de datos" });
            }

           var resultado = await _signInManager.CheckPasswordSignInAsync(usuario!, request.Password!, false);


            if (resultado.Succeeded) 
            { 
                return TransformerToUserToUserDto(usuario!);
                
            }

            throw new MiddlewareException(HttpStatusCode.Unauthorized, new { mensaje = "Las credenciales son incorrectas" });
        }

        public async Task<UsuarioResponseDto> RegistrarUsuario(UsuarioRegistroRequestDtocs request)
        {

            var existeEmail = await _contexto.Users.Where(x => x.Email == request.Email).AnyAsync();
            var existeUsername = await _contexto.Users.Where(x => x.UserName == request.UserName).AnyAsync();

            if (existeEmail) 
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "El email ya existe en la base de datos" });

            }

            if (existeUsername)
            {
                throw new MiddlewareException(HttpStatusCode.BadRequest, new { mensaje = "El usuario ya existe en la base de datos" });

            }

            var usuario = new Usuario {
         
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Email = request.Email,
                UserName = request.UserName,    

            };

            var resultado = await  _userManager.CreateAsync(usuario, request.Password!);

            if (resultado.Succeeded)
            {
                return TransformerToUserToUserDto(usuario);
            
            }

            throw new Exception("No se pudo registrar el usuario");
        }
    }
}
