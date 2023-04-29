using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GstProject.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Desc { get; set; }
        public string Pack { get; set; }
        public int nbrtache { get; set; }
    }
    public class ProjectTacheModel
    {
        public int? IdTache { get; set; }
        public string LibTache { get; set; }
        public int? Duree { get; set; }
    }
}
