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
            model.Subjects = GetSubjectsByTeacherId(Id);
            model.Classes= GetClassesByTeacherId(Id);    
            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteTeacher(Id);
            return RedirectToAction("Index");

        }

        [Authorize]
        public IActionResult ViewTimetable()
        {   var model = new TeacherViewModel();
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Add(int Id, string Title, string Firstname, string Surname, IEnumerable<int> Subjects, IEnumerable<int> Classes, string Email)
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

        [Authorize]
        [HttpGet]
        public IActionResult ViewTimetable(int Id)
        {
            var model = new TimetableViewModel();
            model.Timetables = db.GetTimetableByTeacherId(Id).ToList();
            model.Subjects = GetSubjectsByTeacherId(Id);
            model.Classes = GetClassesByTeacherId(Id);
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
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

        private List<SelectListItem> GetLessons()
        {
            var lessons = new List<SelectListItem>();
            foreach (var item in db.GetLessons())
            {
                lessons.Add(new SelectListItem
                {
                    Value = item.LessonID.ToString(),
                    Text = item.LessonNo

                });
            }
            return lessons;

        }

        private List<SelectListItem> GetDays()
        {
            // Convert database subjects to viewModel Subjects
            var days = new List<SelectListItem>();
            foreach (var item in db.GetDays())
            {
                days.Add(new SelectListItem
                {
                    Value = item.DayID.ToString(),
                    Text = item.Name
                });
            }
            return days;
        }

        private List<SelectListItem> GetSubjectsByTeacherId(int Id)
        {


            // Convert database subjects to viewModel Subjects
            var subjects = new List<SelectListItem>();
            foreach (var item in db.GetSubjects())
            {
                subjects.Add(new SelectListItem
                {
                    Value = item.SubjectID.ToString(),
                    Text = item.Name,
                    Selected = false
                });

            }
            var selectedsubjects = db.GetSubjectsByTeacherId(Id);
            foreach (var s in selectedsubjects)
            {
                subjects.First(x => x.Value == s.SubjectID.ToString()).Selected = true;

            }

            return subjects;

        }

        private List<SelectListItem> GetClassesByTeacherId(int Id)
        {


            // Convert database subjects to viewModel Subjects
            var classes = new List<SelectListItem>();
            foreach (var item in db.GetClasses())
            {
                classes.Add(new SelectListItem
                {
                    Value = item.ClassID.ToString(),
                    Text = item.Name,
                    Selected = false
                });

            }
            var selectedclasses = db.GetClassesByTeacherId(Id);
            foreach (var c in selectedclasses)
            {
                classes.First(x => x.Value == c.ClassID.ToString()).Selected = true;

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
