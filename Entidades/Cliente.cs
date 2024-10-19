namespace d_angela_variedades.Entidades
{
    public class Cliente
    {
        public Guid IdCliente { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Nota { get; set; }
        public int GrupoId { get; set; }
        public int EmpresaId { get; set; }
    }
}
