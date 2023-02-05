using Datos.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Importante
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorLibro>().HasKey(x => new { x.AutorId, x.LibroId });
            modelBuilder.Entity<EditorialLibro>().HasKey(x => new { x.EditorialId, x.LibroId });
            modelBuilder.Entity<GuardadoLibro>().HasKey(x => new { x.GuardadoId, x.LibroId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Autor> Autor { get; set; }

        public DbSet<Libro> Libro { get; set; }

        public DbSet<Editorial> Editorial { get; set; }

        public DbSet<Comentario> Comentario { get; set; }

        public DbSet<Guardados> Guardado { get; set; }

        public DbSet<AutorLibro> AutorLibro { get; set; }

        public DbSet<EditorialLibro> EditorialLibro { get; set; }

        public DbSet<GuardadoLibro> GuardadoLibro { get; set; }
    }
}
