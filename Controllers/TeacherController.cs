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
            model.Subjects = GetSubjects();
            model.Classes= GetClasses();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetTeacherById(Id);
            model.Subjects = GetSubjects();
            model.Classes= GetClasses();    
            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteTeacher(Id);
            return RedirectToAction("Index");

        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {
            GetSubjects();
            GetClasses();
            db.AddTeacher(Title, Firstname, Surname, Subjects, Classes, Email);
            return View(new TeacherViewModel());
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
        {

            db.UpdateTeacherById(Id, Title, Firstname, Surname, Subjects, Classes,  Email);
            return View(new TeacherViewModel());
        }
        
        private List<SelectListItem> GetSubjects()
        {
            // Convert database subjects to viewModel Subjects
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

        private List<SelectListItem> GetClasses()
        {
            // Convert database subjects to viewModel Subjects
            var classes = new List<SelectListItem>();
            foreach (var item in db.GetClasses())
            {
                classes.Add(new SelectListItem
                {
                    Value = item.ClassID.ToString(),
                    Text = item.Name
                });
            }
            return classes;
        }

        private TeacherViewModel GetTeacherById(int Id)
        {
            // Convert database teacher to viewModel teacher
            var dbTeacher = db.GetTeacherById(Id);
            return new TeacherViewModel
            {
                TeacherID= dbTeacher.TeacherID,
                Title = dbTeacher.Title,
                Firstname = dbTeacher.Firstname,
                Surname = dbTeacher.Surname,
                Email = dbTeacher.Email,

            } ;
        }

    }
}
