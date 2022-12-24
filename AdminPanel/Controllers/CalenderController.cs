using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
namespace AdminPanel.Controllers
{
    public class CalenderController: Controller
    {
        ApplicationContext _context;
            public CalenderController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string dateEvent,string nameEvent)
        {
            var year = "";
            var month = "";
            var day = "";
            
            foreach(var i in dateEvent.Split()){
                 year = i.Split('-')[2];
                 month = i.Split('-')[1];
                 day = i.Split('-')[0];
            }
            Calender calender = new Calender() { Month = month,Year=year };
            await _context.Calender.AddAsync(calender);
            await _context.SaveChangesAsync();
            Events events = new Events() { Day = day,Name=nameEvent,Calender =calender};
            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
