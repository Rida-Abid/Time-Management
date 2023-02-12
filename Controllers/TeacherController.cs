using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.ViewModels;

namespace TTMS.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DataController db;
        public TeacherController() 
        {
            db = new DataController();
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(db.GetTeachers());
        }
        [Authorize]
        public IActionResult Add()
        {
            var model = new TeacherViewModel();
            model.Subjects = new List<SelectListItem>();
            foreach(var item in db.GetSubjects())
            {
                model.Subjects.Add(new SelectListItem
                {
                    Value = item.SubjectID.ToString(),
                });
            }
            model.Subjects = db.GetSubjects(); 
            // db.GetClasses
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(db.GetTeacherById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteTeacher(Id);
            return RedirectToAction("Index");

        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Title, string Firstname, string Surname, string Subject, string Email)
        {
            db.GetSubjects();
            db.AddTeacher(Title, Firstname, Surname, Subject, Email);
            return View();
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Title, string Firstname, string Surname, string Subject, string Email)
        {
            db.UpdateTeacherById(Id, Title, Firstname, Surname, Subject,  Email);
            return View();
        }
        

    }
}
