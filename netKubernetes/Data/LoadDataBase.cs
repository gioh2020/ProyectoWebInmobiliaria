using Microsoft.AspNetCore.Identity;
using netKubernetes.Models;

namespace netKubernetes.Data
{
    public class LoadDataBase
    {
        public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { 
                    Nombre = "Giovanny",
                    Apellido = "Vasquez",    
                    Email = "gioh2020@gmail.com",   
                    UserName = "Gioh",    
                    Telefono = "3016444226"
                
                };
                await usuarioManager.CreateAsync(usuario, "Deadpool2367*");
            
            
            }
            if (!context.Inmuebles!.Any())
            {
                context.Inmuebles!.AddRange(

                      new Inmueble
                     {
                         Nombre = "Casa de playa",
                         Direccion = "AV 12345 calle 11",
                         Precio = 2000M,
                         FechaCreacion = DateTime.Now,
                       

                     },
                      new Inmueble
                      {
                           Nombre = "Casa de bosque",
                           Direccion = "AV 5423 calle 20",
                           Precio = 2000M,
                           FechaCreacion = DateTime.Now,


                      }

                );               
            }

            context.SaveChanges();

        }
    }
}
