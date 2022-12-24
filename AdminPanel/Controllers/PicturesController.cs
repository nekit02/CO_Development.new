using AdminPanel.Models;
using AdminPanel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class PicturesController : Controller
    {
        PicturesService PicturesService;
        public PicturesController(ApplicationContext context, IWebHostEnvironment env)
        {
            PicturesService = new PicturesService(context);
        }
        public IActionResult PicturesEdit(int newsId)
        {
            var listPictures = PicturesService.GetAll(newsId);
            return View(listPictures);
        }
        public IActionResult DeletePicture([FromForm] Models.File pictures)
        {
            //Получить айди фото для удаления
            PicturesService.Delete(pictures);
            return RedirectToAction("Index", "News");

        }
        [HttpPost]
        public IActionResult DoMainPicture(int id)
        {
            
            PicturesService.DoMain(id);
            return RedirectToAction("Index", "News");
        }
    }
}
