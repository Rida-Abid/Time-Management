using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using TTMS.Models;
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

            return View(GetTimetables());
        }

        [Authorize]
        public IActionResult Add(int Id)
        {
            var model = new TimetableViewModel();
            model.Teachers = GetTeachers();
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var dbLesson = db.GetTimetableById(Id);
            var model = new TimetableViewModel();
            model.Teachers = GetTeachers();
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
        public IActionResult Add(int Id, int TeacherID, int SubjectID, int ClassID, int LessonID, int DayID)
        {
            var model = new TimetableViewModel();
            db.AddTimetable( TeacherID, SubjectID, ClassID, LessonID, DayID);
            model.Teachers = GetTeachers();
            model.Subjects = GetSubjectsByTeacherId(Id);
            model.Classes = GetClassesByTeacherId(Id);
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, int TeacherID, int SubjectID, int ClassID, int LessonID, int DayID)
        {
            var model = new TimetableViewModel();
            db.UpdateTimetableById(Id, TeacherID, SubjectID, ClassID, LessonID, DayID);
            model.Teachers = GetTeachers();
            model.Subjects = GetSubjectsByTeacherId(Id);
            model.Classes = GetClassesByTeacherId(Id);
            model.Lessons = GetLessons();
            model.Days = GetDays();
            return View(model);
        }

        public List<TimetableViewModel> GetTimetables()
        {
            var timetables = new List<TimetableViewModel>();
            foreach (var item in db.GetTimetables())
            {
                timetables.Add(new TimetableViewModel
                {
                    TimetableID = item.TimetableID,
                    Teacher = item.Teacher,
                    Day = item.Day,
                    Lesson = item.Lesson

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
            var x= 0;
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

        //private TimetableViewModel GetTimetableById(int Id)
        //{
        //    var dbLesson = db.GetTimetableById(Id);
        //    return new TimetableViewModel
        //    {
        //        TimetableID = dbLesson.TimetableID,
        //        Name = dbLesson.Name

        //    };
        //}






    }
   
}