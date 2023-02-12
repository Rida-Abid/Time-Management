using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
            return View();
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(db.GetLessonById(Id));
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
            db.AddLesson(LessonNo, Duration);
            return View();
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string LessonNo, decimal Duration)
        {
            db.UpdateLessonById(Id, LessonNo, Duration);
            return View();
        }


        



    }
   
}