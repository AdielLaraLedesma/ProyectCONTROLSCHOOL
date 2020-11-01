using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Services.Models;
using System.Data.SqlClient;

namespace SCHOOLCONTROL.Services.DAO
{
    public class AdminContext : DbContext
    {
        public AdminContext() 
            : base(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString),true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove(new System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention());


            modelBuilder.Entity<Models.ESTUDIANTE>().ToTable("ESTUDIANTE");
            modelBuilder.Entity<Models.PROFESOR>().ToTable("PROFESOR");
            modelBuilder.Entity<Models.CURSO>().ToTable("CURSO");
            modelBuilder.Entity<Models.CURSOESTUDIANTE>().ToTable("CURSOESTUDIANTE");

            modelBuilder.Entity<Models.CURSO>().HasRequired(e => e.PROFESOR_OBJ).WithMany().HasForeignKey(e => e.IDPROFESOR);
            //modelBuilder.Entity<Models.CURSO>().HasOptional(e => e.CURSO_ANTERIOR_OBJ).WithMany().HasForeignKey(e => e.CURSOANTERIOR);
            //modelBuilder.Entity<Models.CURSO>().HasOptional(e => e.CURSO_SIGUIENTE_OBJ).WithMany().HasForeignKey(e => e.CURSOSIGUIENTE);

            modelBuilder.Entity<Models.CURSOESTUDIANTE>().HasRequired(e => e.CURSO_OBJ).WithMany().HasForeignKey(e => e.IDCURSO);
            modelBuilder.Entity<Models.CURSOESTUDIANTE>().HasRequired(e => e.ESTUDIANTE_OBJ).WithMany().HasForeignKey(e => e.IDESTUDIANTE);

            



            var user = new SqlConnectionStringBuilder(Database.Connection.ConnectionString).UserID;
            modelBuilder.HasDefaultSchema(user);
        }
        public DbSet<Models.ESTUDIANTE> Estudiantes { get; set; }
        public DbSet<Models.PROFESOR> Profesores { get; set; }
        public DbSet<Models.CURSO> Cursos { get; set; }
        public DbSet<Models.CURSOESTUDIANTE> CursosEstudiantes { get; set; }
    }
}
