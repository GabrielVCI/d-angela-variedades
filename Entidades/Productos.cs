namespace d_angela_variedades.Entidades
{
    public class Productos
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public Categoria Categoria { get; set; }
        public int IdCategoria { get; set; }
        public Subcategoria Subcategoria { get; set; }
        public int IdSubCategoria { get; set; }
    }
}
