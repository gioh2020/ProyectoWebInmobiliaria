using Microsoft.AspNetCore.Mvc;
using netKubernetes.Data.Inmuebles;
using AutoMapper;
using netKubernetes.Dto.InmuebleDto;
using netKubernetes.Middleware;
using System.Net;
using netKubernetes.Models;

namespace netKubernetes.Controllers
{
    public class InmuebleController : ControllerBase
    {
        private readonly IInmubleRepositorycs _repository;
        private IMapper _mapper;
        public InmuebleController(
            IInmubleRepositorycs repository
            , IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InmuebleResponseDto>>> getInmuebles()
        {
            var inmuebles = await _repository.GetAllInmuebles();
            return Ok(_mapper.Map<IEnumerable<InmuebleResponseDto>>(inmuebles));
        }

        [HttpGet("{id}", Name = "GetInmuebleById")]
        public async Task<ActionResult<IEnumerable<InmuebleResponseDto>>> getInmuebleById(int id)
        {
            var inmueble = await _repository.GetInmuebleById(id);

            if (inmueble is null)
            {
                throw new MiddlewareException(
                  HttpStatusCode.NotFound,
                  new { mensaje = $"No se encontro el inmuble con ese id" }
                );
            }

            return Ok(_mapper.Map<IEnumerable<InmuebleResponseDto>>(inmueble));
        }
        [HttpPost]
        public async Task<ActionResult<InmuebleResponseDto>> CreateInmueble([FromBody] InmuebleRequestDto inmueble)
        {
            var inmubleModel =  _mapper.Map<Inmueble>(inmueble);
           await _repository.CreateInmueble(inmubleModel);
           await _repository.SaveChanges();

            var inmuebleResponse = _mapper.Map<InmuebleRequestDto>(inmubleModel);

            return CreatedAtRoute(nameof(getInmuebleById), new { inmuebleResponse.Id }, inmuebleResponse);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInmueble(int id)
        { 
            await _repository.DeleteInmueble(id);
            await _repository.SaveChanges();
            return Ok();
        }



    }
}
