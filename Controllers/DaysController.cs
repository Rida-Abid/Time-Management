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
    public class DaysController : Controller
    {
        private readonly DataController db;
        public DaysController()
        {
            db = new DataController();
        }

        
        [Authorize]
        public IActionResult Index()
        {
            var model = new DaysViewModel();
            model.Days = GetDays();
            return View(model);
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

        private DaysViewModel GetDaysById(int Id)
        {
            // Convert database teacher to viewModel teacher
            var dbClass = db.GetDaysById(Id);
            return new DaysViewModel
            {
                DayID = dbClass.DayID,
                Name = dbClass.Name,
               
            };
        }


    }
    
}

