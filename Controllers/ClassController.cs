using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using TTMS.ViewModels;

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
            var model = new ClassViewModel();
            model.Classes = GetClasses();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetClassById(Id);
            model.Classes = GetClasses();
            return View(model);
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
            GetClasses();
            db.AddClass(Name);
            return View(new ClassViewModel());
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            db.UpdateClassById(Id, Name);
            return View();
        }

        private List<SelectListItem> GetClasses()
        {
            // Convert database subjects to viewModel Subjects
            var lessons = new List<SelectListItem>();
            foreach (var item in db.GetClasses())
            {
                lessons.Add(new SelectListItem
                {
                    Value = item.ClassID.ToString(),
                    Text = item.Name
                });
            }
            return lessons;
        }

        private ClassViewModel GetClassById(int Id)
        {
            // Convert database teacher to viewModel teacher
            var dbClass = db.GetClassById(Id);
            return new ClassViewModel
            {
                ClassID = dbClass.ClassID,
                Name = dbClass.Name,
               
            };
        }


    }
    
}

