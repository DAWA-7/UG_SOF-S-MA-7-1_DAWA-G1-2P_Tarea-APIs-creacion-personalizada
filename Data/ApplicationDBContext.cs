using APIClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace APIClientes.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasOne<Ciudad>(s => s.Ciudad)
                .WithMany(g => g.Clientes)
                .HasForeignKey(s => s.Ciudad_Id);
        }*/

        public DbSet<Cliente> Cliente{ get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }

     

        //public DbSet<Proveedor> Proveedores { get; set; }
    }
}
