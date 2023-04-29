
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GstProject.Models
{
    public class Tache
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Desc { get; set; }
    }
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string? Libelle { get; set; }
        [Display(Name = "Description")]
        public string? Desc { get; set; }
        public Pack? Pack { get; set; }
        [Display(Name = "Pack")]
        [ForeignKey("Pack")]
        public int? IdPack { get; set; }
        public List<ProjectTache>? projectTaches { get; set; }
    }
    public class ProjectTache
    {
        public Project? Project { get; set; }
        public Tache? Tache { get; set; }

        [ForeignKey("Tache")]
        public int? IdTache { get; set; }
        [ForeignKey("Project")]
        public int? IdProject { get; set; }
        public int? Duree { get; set; }
    }
    public class Pack
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Desc { get; set; }
    }
}