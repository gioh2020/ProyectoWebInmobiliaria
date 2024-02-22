namespace netKubernetes.Dto.InmuebleDto
{
    public class InmuebleResponseDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Precio { get; set; }
        public string? Picture { get; set; }
        public DateTime? FechaCreacion { get; set; }

    }
}
