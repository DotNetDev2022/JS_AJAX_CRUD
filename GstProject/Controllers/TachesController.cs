using GstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GstProject.Controllers
{
    public class TachesController : Controller
    {
        public MyContext db;
        public TachesController(MyContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var taches = db.Taches.ToList();
            return View(taches);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tache t)
        {
            if (ModelState.IsValid)
            {
                db.Taches.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t);
        }
        public IActionResult Edit(int id = 0)
        {
            //Verfier existance Id
            if (id == 0)
                return RedirectToAction("Index");

            //Rechercher Tache
            var tache = db.Taches.Find(id);

            //Verefier Si Tache Exist
            if (tache == null)
                return RedirectToAction("Index");

            //Passer tache au View
            return View(tache);
        }
        [HttpPost]
        public IActionResult Edit(Tache tache)
        {
            if (ModelState.IsValid)
            {
                db.Taches.Update(tache);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tache);
        }
        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var Emp = db.Taches.Find(id);
            if (Emp == null)
                return RedirectToAction("Index");

            db.Taches.Remove(Emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

