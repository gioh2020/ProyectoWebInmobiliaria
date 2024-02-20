using netKubernetes.Models;

namespace netKubernetes.Data.Inmuebles
{
    public interface IInmubleRepositorycs
    {
        bool SaveChanges();
        IEnumerable<Inmueble> GetAllInmuebles();
        Inmueble GetInmuebleById(int id);
        Task CreateInmueble(Inmueble inmueble);
        void DeleteInmueble(int id);
    }
}
