using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
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
            model.Teachers = GetTeachers();
            model.Subjects = GetSubjects();
            model.Classes = GetClasses();
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetTimetableById(Id);
            model.Teachers = GetTeachers();
            model.Subjects = GetSubjects();
            model.Classes = GetClasses();
            model.Lessons = GetLessons();
            model.Days = GetDays();
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
        public IActionResult Add(string Name, int TeacherID, int SubjectID, int ClassID, int LessonID, int DayID)
        {
            var model = new TimetableViewModel();
            db.AddTimetable(Name, TeacherID, SubjectID, ClassID, LessonID, DayID);
            return View(model);
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name, int TeacherID, int SubjectID, int ClassID, int LessonID, int DayID)
        {
            var model = new TimetableViewModel();
            db.UpdateTimetableById(Id, Name, TeacherID, SubjectID, ClassID, LessonID, DayID);
            return View(model);
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

        private List<SelectListItem> GetTeachers()
        {
            var teachers = new List<SelectListItem>();
            foreach (var item in db.GetTeachers())
            {
                teachers.Add(new SelectListItem
                {
                    Value = item.TeacherID.ToString(),
                    Text = item.Firstname
                });
            }
            return teachers;
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