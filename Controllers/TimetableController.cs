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
            model.Timetables = GetTimetables();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetTimetableById(Id); 
            model.Timetables = GetTimetables();
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
            var lessons = new List<SelectListItem>();
            foreach (var item in db.GetTimetables())
            {
                lessons.Add(new SelectListItem
                {
                    Value = item.TimetableID.ToString(),
                    Text = item.Name

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