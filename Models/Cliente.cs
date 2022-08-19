using System.ComponentModel.DataAnnotations;

namespace APIClientes.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Edad { get; set; }

        //public int Ciudad_Id { get; set; }
        [Required]
        public Ciudad Ciudad { get; set; }
    }

    public class Ciudad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre_Ciudad { get; set; }

        //public ICollection<Cliente> Clientes { get; set; }
    }
}
