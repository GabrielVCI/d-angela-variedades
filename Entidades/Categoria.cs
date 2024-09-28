namespace d_angela_variedades.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public int EmpresaId { get; set; }
        public List<Subcategoria> Subcategorias { get; set; }
    }
}
