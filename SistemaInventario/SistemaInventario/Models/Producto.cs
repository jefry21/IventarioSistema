namespace SistemaInventario.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int MInimoStock { get; set; }
        public int Activo { get; set; }
    }
}
