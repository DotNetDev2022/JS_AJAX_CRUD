using Microsoft.EntityFrameworkCore;

namespace GstProject.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTache> ProjectTaches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTache>().HasKey(e => new { e.IdTache, e.IdProject });

            modelBuilder.Entity<Pack>().HasData(
                new Pack
                {
                    Id = 1,
                    Libelle = "Pack1",
                    Desc = "Desc1",
                },
                new Pack
                {
                    Id = 2,
                    Libelle = "Pack2",
                    Desc = "Desc2"
                }
                );

            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Libelle = "Project1",
                    Desc = "Desc1",
                    IdPack = 1
                },
                new Project
                {
                    Id = 2,
                    Libelle = "Project2",
                    Desc = "Desc2",
                    IdPack = 1
                }
                );

            modelBuilder.Entity<Tache>().HasData(
                new Tache
                {
                    Id = 1,
                    Libelle = "Tache1",
                    Desc = "Desc1"
                },
                new Tache
                {
                    Id = 2,
                    Libelle = "Tache2",
                    Desc = "Desc2"
                }
                );
        }
    }
}
