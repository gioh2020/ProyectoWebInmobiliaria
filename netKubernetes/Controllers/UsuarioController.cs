using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netKubernetes.Data.Usuarios;
using netKubernetes.Dto.UsuarioDto;

namespace netKubernetes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository suarioRepository)
        {
            _repository = suarioRepository;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioResponseDto>> Login([FromBody] UsuarioLoginRequestDto request)
        {
            return await _repository.Login(request);

        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioResponseDto>> Registrar([FromBody] UsuarioRegistroRequestDtocs request)
        {
            return await _repository.RegistrarUsuario(request);

        }

        [HttpGet]
        public async Task<ActionResult<UsuarioResponseDto>> DevolverUsuario([FromBody] UsuarioLoginRequestDto request)
        {
            return await _repository.GetUsuario();

        }
    }
}
