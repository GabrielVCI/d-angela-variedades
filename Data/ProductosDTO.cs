using d_angela_variedades.Entidades;

namespace d_angela_variedades.Data
{
    public class ProductosDTO
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdSubCategoria { get; set; }
    }
}
