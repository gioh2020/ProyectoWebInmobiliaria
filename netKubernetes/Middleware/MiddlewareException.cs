using System.Net;

namespace netKubernetes.Middleware
{
    public class MiddlewareException: Exception
    {

        public HttpStatusCode Codigo { get; set; }  
        public Object? Errores { get; set;}
        public MiddlewareException(HttpStatusCode codigo, object? errores = null) { 
        
            Codigo = codigo;    
            Errores = errores;
        
        }

    }
}
