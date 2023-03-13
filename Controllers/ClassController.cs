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
        public IActionResult ViewTimetable()
        {
            var model = new TeacherViewModel();
            return View(model);
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
            return View(new ClassViewModel());
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewTimetable(int Id)
        {
            var model = new TimetableViewModel();
            model.Timetables = db.GetTimetableByClassId(Id).ToList();
            model.Subjects = GetSubjectsByTeacherId(Id);
            model.Classes = GetClassesByTeacherId(Id);
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
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
            foreach (var item in db.GetSubjectsByTeacherId(Id))
            {
                subjects.Add(new SelectListItem
                {
                    Value = item.SubjectID.ToString(),
                    Text = item.Name
                });
            }
            return subjects;
        }

        private List<SelectListItem> GetClassesByTeacherId(int Id)
        {
            // Convert database subjects to viewModel Subjects
            var classes = new List<SelectListItem>();
            foreach (var item in db.GetClassesByTeacherId(Id))
            {
                classes.Add(new SelectListItem
                {
                    Value = item.ClassID.ToString(),
                    Text = item.Name
                });
            }
            return classes;
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

