namespace d_angela_variedades.Entidades
{
    public class Ventas
    {
        public int Id_Venta { get; set; }
        public string Cliente { get; set; }
        public string Cod_Venta { get; set; }
        public string Desc_Prod { get; set; }
        public float TotalCompra { get; set; }
        public Productos Productos { get; set; }
        public Guid ProductoId { get; set; }
    }
}
