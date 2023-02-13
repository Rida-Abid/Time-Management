using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using TTMS.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace TTMS.Controllers
{
    public class SubjectController : Controller
    {
        private readonly DataController db;
        public SubjectController()
        {
            db = new DataController();
        }

      
        [Authorize]
        public IActionResult Index()
        {

            return View(db.GetSubjects());
        }

        [Authorize]
        public IActionResult Add()
        {
            var model = new SubjectViewModel();
            model.Subjects = GetSubjects();
            return View(model);

        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetSubjectById(Id);
            model.Subjects = GetSubjects();
            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteSubject(Id);
            return RedirectToAction("Index");
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            GetSubjects();
            db.AddSubject( Name);
            return View();
        }
       
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            db.UpdateSubjectById(Id, Name);
            return View();
        }

        public List<SelectListItem> GetSubjects()
        {
           var subjects = new List<SelectListItem>();
           foreach (var item in db.GetSubjects())
           {
                subjects.Add(new SelectListItem
                {
                    Value = item.SubjectID.ToString(),
                    Text = item.Name
                });
           }
                
           return subjects;
        }

        public SubjectViewModel GetSubjectById(int Id)
        {
            var model = new SubjectViewModel();
            var dbSubject = db.GetSubjectById(Id);
            return new SubjectViewModel
            {
                SubjectID = dbSubject.SubjectID,
                Name = dbSubject.Name

            };
        }
        

    }
    
}