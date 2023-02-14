using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.ViewModels;

namespace TTMS.Controllers
{
    public class TimetableController : Controller
    {
        private readonly DataController db;
        public TimetableController()
        {
            db = new DataController();
        }

        [Authorize]
        public IActionResult Index()
        {

            return View(db.GetTimetables());
        }

        [Authorize]
        public IActionResult Add()
        {
            var model = new TimetableViewModel();
            model.Subjects = GetSubjects();
            model.Classes = GetClasses();
            model.Lessons = GetLessons();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetTimetableById(Id);
            model.Subjects = GetSubjects();
            model.Classes = GetClasses();
            model.Lessons = GetLessons();
            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteTimetable(Id);
            return RedirectToAction("Index");
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Add(string Name)
        {
            GetTimetables();
            GetSubjects();
            GetClasses();
            GetLessons();
            db.AddTimetable(Name);
            return View(new TimetableViewModel());
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            db.UpdatetimetableById(Id, Name);
            return View();
        }

        public List<SelectListItem> GetTimetables()
        {
            var timetables = new List<SelectListItem>();
            foreach (var item in db.GetTimetables())
            {
                timetables.Add(new SelectListItem
                {
                    Value = item.TimetableID.ToString(),
                    Text = item.Name

                });
            }
            return timetables;
            
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
            // Convert database classes to viewModel Classes
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

        private TimetableViewModel GetTimetableById(int Id)
        {
            var dbLesson = db.GetTimetableById(Id);
            return new TimetableViewModel
            {
                TimetableID = dbLesson.TimetableID,
                Name = dbLesson.Name
               
            };
        }






    }
   
}