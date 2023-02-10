using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using TTMS.Models;

namespace TTMS.Controllers
{
    public class SubjectController : Controller
    {
        private readonly DataController db;
        public SubjectController()
        {
            db = new DataController();
        }

      
        [Authorize]
        public IActionResult Index()
        {

            return View(db.GetSubjects());
        }

        [Authorize]
        public IActionResult Add()
        {
            
            return View();

        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            return View(db.GetSubjectById(Id));
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            db.DeleteSubject(Id);
            return RedirectToAction("Index");
        }
      
        [Authorize]
        [HttpPost]
        public IActionResult Add(int Id, string Name)
        {
           db. AddSubject( Id ,Name);
            return View();
        }
       
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            db.UpdateSubjectById(Id, Name);
            return View();
        }


        

    }
    
}