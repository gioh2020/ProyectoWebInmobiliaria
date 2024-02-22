using netKubernetes.Models;

namespace netKubernetes.Data.Inmuebles
{
    public interface IInmubleRepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Inmueble>> GetAllInmuebles();
        Task<Inmueble> GetInmuebleById(int id);
        Task CreateInmueble(Inmueble inmueble);
        Task DeleteInmueble(int id);
    }
}
