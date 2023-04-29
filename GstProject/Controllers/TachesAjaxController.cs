using GstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GstProject.Controllers
{
    public class TachesAjaxController : Controller
    {
        public MyContext db;
        public TachesAjaxController(MyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetTaches()
        {
            try
            {
                List<Tache> lst = db.Taches.ToList();
                return Json(new { R = "Y", M = $"Vous-avez {lst.Count()} Taches", Taches = lst });
            }
            catch (Exception exception)
            {
                return Json(new { R = "N", M = exception.Message });
            }
        }
        public IActionResult GetTache(int id = 0)
        {
            try
            {
                var tache = db.Taches.Find(id);
                if (tache != null)
                    return Json(new { R = "Y", M = $"Tache {id} Exist.", Tache = tache });
                else
                    return Json(new { R = "N", M = $"Tache {id} Not Exist." });
            }
            catch (Exception exception)
            {
                return Json(new { R = "N", M = exception.Message });
            }
        }
        [HttpPost]
        public IActionResult Create(Tache tache)
        {
            try
            {
                db.Taches.Add(tache);
                db.SaveChanges();
                return Json(new { R = "Y", M = "Tache Bien Ajouter" });
            }
            catch (Exception exception)
            {
                return Json(new { R = "N", M = exception.Message });
            }
        }
        [HttpPost]
        public IActionResult Edit(Tache tache)
        {
            try
            {
                db.Taches.Update(tache);
                db.SaveChanges();
                return Json(new { R = "Y", M = "Tache Bien Modifier" });
            }
            catch (Exception exception)
            {
                return Json(new { R = "N", M = exception.Message });
            }
        }
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var Emp = db.Taches.Find(id);
                if (Emp == null)
                    return Json(new { R = "N", M = "Tache Non-Exist" });

                db.Taches.Remove(Emp);
                db.SaveChanges();

                return Json(new { R = "Y", M = "Tache Supprimer Avec Seccuss" });
            }
            catch (Exception exception)
            {
                return Json(new { R = "N", M = exception.Message });
            }
        }
    }
}
