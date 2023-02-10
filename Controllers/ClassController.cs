using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TTMS.Controllers
{
    public class ClassController : Controller
    {
        private readonly DataController db;
        public ClassController()
        {
            db = new DataController();
        }

        
        [Authorize]
        public IActionResult Index()
        {

            return View(db.GetClasses());
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(db.GetClassById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteClass(Id);
            return RedirectToAction("Index");
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            db.AddClass(Name);
            return View();
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            db.UpdateClassById(Id, Name);
            return View();
        }
              

       

    }
    
}

