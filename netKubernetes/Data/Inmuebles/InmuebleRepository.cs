﻿using Microsoft.AspNetCore.Identity;
using netKubernetes.Models;
using netKubernetes.Token;

namespace netKubernetes.Data.Inmuebles
{
    public class InmuebleRepository : IInmubleRepositorycs
    {
        private readonly AppDbContext _contexto;
        private readonly IUsuarioSesion _usuarioSesion;
        private readonly UserManager<Usuario> _userManager;

        public InmuebleRepository(
            AppDbContext contexto,
            IUsuarioSesion sesion,
            UserManager<Usuario> userManager

        ) 

        {
            _contexto = contexto;
            _usuarioSesion = sesion;
            _userManager = userManager;
        }
        public async Task CreateInmueble(Inmueble inmueble)
        {
            var usuario = await  _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

            inmueble.FechaCreacion = DateTime.Now;
            inmueble.UsuarioId = Guid.Parse(usuario!.Id);

            _contexto.Inmuebles!.Add( inmueble );
        }

        public void DeleteInmueble(int id)
        {
            var inmueble = _contexto.Inmuebles!.FirstOrDefault( x => x.Id == id);
            _contexto.Inmuebles!.Remove(inmueble!);
        }

        public IEnumerable<Inmueble> GetAllInmuebles()
        {
            return _contexto.Inmuebles!.ToList();
        }

        public Inmueble GetInmuebleById(int id)
        {
            return _contexto.Inmuebles!.FirstOrDefault(x => x.Id == id); 
        }

        public bool SaveChanges()
        {
            return (_contexto.SaveChanges() >= 0);
        }
    }
}
