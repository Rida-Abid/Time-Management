using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTMS.ViewModels;

namespace TTMS.Controllers
{
    public class LessonController : Controller
    {
        private readonly DataController db;
        public LessonController()
        {
            db = new DataController();
        }

        [Authorize]
        public IActionResult Index()
        {

            return View(db.GetLessons());
        }

        [Authorize]
        public IActionResult Add()
        {
            var model = new LessonViewModel();
            model.Lessons = GetLessons();
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var model = GetLessonById(Id); 
            model.Lessons = GetLessons();
            return View(model);
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteLesson(Id);
            return RedirectToAction("Index");
        }
        

        [Authorize]
        [HttpPost]
        public IActionResult Add(string LessonNo, decimal Duration)
        {
            GetLessons();
            db.AddLesson(LessonNo, Duration);
            return View(new LessonViewModel());
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string LessonNo, decimal Duration)
        {
            db.UpdateLessonById(Id, LessonNo, Duration);
            return View(new LessonViewModel());
        }

        public List<SelectListItem> GetLessons()
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

        private LessonViewModel GetLessonById(int Id)
        {
            var dbLesson = db.GetLessonById(Id);
            return new LessonViewModel
            {
                LessonID = dbLesson.LessonID,
                LessonNo = dbLesson.LessonNo,
                Duration = dbLesson.Duration
               
            };
        }






    }
   
}