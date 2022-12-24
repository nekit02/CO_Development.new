
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
        PicturesService PicturesService;
        public TeacherController(ApplicationContext context, IWebHostEnvironment env)
        {
            _environment = env;
            TeacherService = new TeacherService(context);
            PicturesService = new PicturesService(context);
        }

        public IActionResult IndexTeacher()
        {
            var listTeachers = TeacherService.GetAll() ;
            return View(listTeachers);
        }
        //

        public IActionResult TD(int TeachersId)
        {
            var listTeachers = TeacherService.GetDetails(TeachersId);
            return View(listTeachers);
        }

        public IActionResult DeleteTeacher([FromForm] Teachers teachers)
        {
            //Получить айди новости для удаления
            TeacherService.DeleteTeachers(teachers);
            return RedirectToAction("IndexTeacher");
        }
        public IActionResult Edit([FromForm] Teachers teacher)
        {
            TeacherService.Edit(teacher);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CreateTeacher(string teacherName, string teacherLessons,int teacherworkExp,string Teacherdescription, IFormFile uploadedFile)
        {
            //создаем новость через экземпляр класса news
            var teacher = new Teachers { Name = teacherName, Lessons = teacherLessons, WorkExp= teacherworkExp, Description= Teacherdescription };
            var TeacherID = TeacherService.Create(teacher);
      
            var fileName = Guid.NewGuid() + "." + (uploadedFile.FileName.Split('.').Last());
            //путь для хранени файла
            var path = "/img/" + fileName;
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                //копируем изображения в папку wwrooot + path
                uploadedFile.CopyTo(fileStream);
            }
            Models.File pic = new Models.File() { Name = uploadedFile.FileName, FilePath = path, TeacherId = TeacherID };
            PicturesService.Create(pic);
            return RedirectToAction("IndexTeacher");
        }
        [HttpGet]
        public IActionResult CreateTeacher()
        {

            return View();
        }


    }
}
