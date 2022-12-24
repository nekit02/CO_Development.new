using AdminPanel.DbModels;
using AdminPanel.Models;
using AdminPanel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class TeacherController : Controller
    {
        TeacherService TeacherService;
        IWebHostEnvironment _environment;
        public TeacherController(ApplicationContext context, IWebHostEnvironment env)
        {
            _environment = env;
            TeacherService = new TeacherService(context);
        }
        public IActionResult IndexTeacher()
        {
            var listTeachers = TeacherService.GetAll();
            return View(listTeachers);
        }
        //

        public IActionResult TD(int TeachersId)
        {
            var listTeachers = TeacherService.GetDetails(TeachersId);
            return View(listTeachers);
        }

        [HttpPost]
        public IActionResult DeleteTeacher([FromForm] Teachers teachers)
        {
            //Получить айди новости для удаления
            TeacherService.DeleteTeachers(teachers);
            return RedirectToAction("IndexTeacher");
        }

        public IActionResult EditTeacher(int teacherId)
        {
            //получить из сервиса 1 новость по айди
            var OneTeacher = TeacherService.GetDetails(teacherId);
            //передать во вьюшку
            return View(OneTeacher);
        }

        [HttpPost]
        public IActionResult EditTeacher([FromForm] Teachers teachers)
        {
            TeacherService.Edit(teachers);
            return RedirectToAction("IndexTeacher");
        }
    }
}
