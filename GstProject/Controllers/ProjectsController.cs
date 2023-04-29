using GstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace GstProject.Controllers
{
    public class ProjectsController : Controller
    {
        public MyContext db;
        public readonly ILogger _logger;
        public ProjectsController(MyContext _db, ILogger<ProjectsController> logger)
        {
            db = _db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var lst = db.Projects.Join(db.Packs, pr => pr.IdPack, p => p.Id, (pr, p) => new { Proj = pr, Pack = p })
                .Select(x => new ProjectModel {
                    Id = x.Proj.Id,
                    Libelle = x.Proj.Libelle,
                    Desc = x.Proj.Desc,
                    Pack = x.Pack.Libelle,
                    nbrtache = db.ProjectTaches.Where(o => o.IdProject == x.Proj.Id).Count()
                })
                .ToList();
            return View(lst);
        }
        public IActionResult Create()
        {
            ViewBag.Packs = new SelectList(db.Packs.ToList(), "Id", "Libelle");
            ViewBag.Taches = new SelectList(db.Taches.ToList(), "Id", "Libelle");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Project p, string[] id_taches, string[] durees)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(p);
                db.SaveChanges();

                int id = p.Id;
                for (int i = 0; i < id_taches.Length; i++)
                {
                    db.ProjectTaches.Add(new ProjectTache()
                    {
                        IdProject = id,
                        IdTache = int.Parse(id_taches[i]),
                        Duree = int.Parse(durees[i]),
                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Packs = new SelectList(db.Packs.ToList(), "Id", "Libelle");
            ViewBag.Taches = new SelectList(db.Taches.ToList(), "Id", "Libelle");
            return View(p);
        }
        public IActionResult Edit(int id = 0)
        {
            //Verfier existance Id
            if (id == 0)
                return RedirectToAction("Index");

            //Rechercher project
            var project = db.Projects.Find(id);

            //Verefier Si project Exist
            if (project == null)
                return RedirectToAction("Index");
            var lst=db.ProjectTaches.Join(db.Taches, pt => pt.IdTache, t => t.Id,
                (pt, t) => new { Pt = pt, Tache = t })
                .Where(x => x.Pt.IdProject == id)
                .Select(x => new ProjectTacheModel
                {
                    IdTache = x.Pt.IdTache,
                    LibTache = x.Tache.Libelle,
                    Duree = x.Pt.Duree
                })
                .ToList();
            ViewBag.ProjectTache = JsonConvert.SerializeObject(lst);

            ViewBag.Packs = new SelectList(db.Packs.ToList(), "Id", "Libelle");
            ViewBag.Taches = new SelectList(db.Taches.ToList(), "Id", "Libelle");
            //Passer project au View
            return View(project);
        }
        [HttpPost]
        public IActionResult Edit(Project p, string[] id_taches, string[] durees)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Update(p);
                db.SaveChanges();

                int id = p.Id;
                var projectTaches = db.ProjectTaches.Where(x=>x.IdProject==id).ToList();
                db.ProjectTaches.RemoveRange(projectTaches);

                for (int i = 0; i < id_taches.Length; i++)
                {
                    db.ProjectTaches.Add(new ProjectTache()
                    {
                        IdProject = id,
                        IdTache = int.Parse(id_taches[i]),
                        Duree = int.Parse(durees[i]),
                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            var lst = db.ProjectTaches.Join(db.Taches, pt => pt.IdTache, t => t.Id,
                (pt, t) => new { Pt = pt, Tache = t })
                .Where(x => x.Pt.IdProject == p.Id)
                .Select(x => new ProjectTacheModel
                {
                    IdTache = x.Pt.IdTache,
                    LibTache = x.Tache.Libelle,
                    Duree = x.Pt.Duree
                })
                .ToList();
            ViewBag.ProjectTache = JsonConvert.SerializeObject(lst);

            ViewBag.Packs = new SelectList(db.Packs.ToList(), "Id", "Libelle");
            ViewBag.Taches = new SelectList(db.Taches.ToList(), "Id", "Libelle");
            return View(p);
        }
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var p = db.Projects.Find(id);
            if (p == null)
                return RedirectToAction("Index");

            db.Projects.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
