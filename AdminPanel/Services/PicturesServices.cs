using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Services
{
    public class PicturesService
    {
        ApplicationContext db;
        public PicturesService(ApplicationContext context)
        {
            db = context;
        }
        public List<Models.File> GetAll(int NewsId)
        {
            return db.Pictures.Where(x => x.NewsId == NewsId).ToList();
        }
        public List<Models.File> GetListForNews(int NewsId)
        {
            //вывести картинки для данной новости
            // новость определяется по полю News Id
            return db.Pictures.Where(x => x.NewsId == NewsId).ToList();
        }
        public Models.File GetDetails(int PicturesId)
        {
            return db.Pictures.Where(x => x.Id == PicturesId).FirstOrDefault();
        }
        public string? Create(Models.File pictures)
        {
            db.Pictures.Add(pictures);
            db.SaveChanges();
            return pictures.FilePath;
        }
        public void Delete(Models.File pictures) //получаю айди новости
        {       //получаю полную картинку
                //создаю переменную с полной картинкой. Данные получаю из базы с помощью метода

            var FullPicture = GetDetails(pictures.Id);
            //проверяю является ли удаленная картинка главной в данной новости
            if (FullPicture.IsMain == true)
            {
                //Найти новость
                var News = db.News.Where(x => x.Id == FullPicture.NewsId).FirstOrDefault();
                //получить все картинки новости
                var AllPictures = GetAll(FullPicture.NewsId);
                //проверка на наличие других картинок
                if (AllPictures.Count == 1)
                {
                    //если нет, то меняю MainPicturesPath на null
                    News.MainPicturePath = null;
                }
                else
                {
                    //Найти первую не главную картинку
                    var NotMainPicture = AllPictures.Where(x => x.IsMain == false).FirstOrDefault();
                    //сделать новую картинку главной
                    NotMainPicture.IsMain = true;
                    //Текущий MainPicturesPath заменить на MainPicturesPath следующей картинки
                    News.MainPicturePath = NotMainPicture.FilePath;
                }
            }
            //если нет то оставляю MainPicturesPath
            db.Pictures.Remove(FullPicture);
            db.SaveChanges();
        }
        public int SetMain(int PictureId)
        {

            //получить картинку из базы
            var MyPicture = db.Pictures.Where(x => x.Id == PictureId).FirstOrDefault();
            //проверяю является ли наша картинка главной. если да, то оставляю ее ID
            if (MyPicture.IsMain == true)
            {
                return MyPicture.Id;
            }

            //Просматриваю есть ли главная картинка
            //Пробегаю по всем картинкам новости и ищу главную
            //получаю айди новости
            var NewsId = MyPicture.NewsId;
            //найти все картинки от этой новости
            var AllPictures = GetListForNews(NewsId);
            //найти старую главную картинку
            var OldMainPicture = AllPictures.Where(x => x.IsMain == true).FirstOrDefault();
            //если есть нахожу ее и снимаю главность
            if (OldMainPicture != null)
            {
                OldMainPicture.IsMain = false;
            }
            //делаю мою картинку главной
            MyPicture.IsMain = true;
            //получить новость
            var news = db.News.Where(x => x.Id == NewsId).FirstOrDefault();
            //сохраняем мссылку на главную картинку в новости
            news.MainPicturePath = MyPicture.FilePath;

            db.SaveChanges();
            return MyPicture.Id;

        }
        public int DoMain(int PictureId)
        {
            //получаю картинку из базы
            var MyPicture = db.Pictures.Where(x => x.Id == PictureId).FirstOrDefault();
            var NewsId = MyPicture.NewsId;
            //найти все картинки от этой новости
            var AllPictures = GetListForNews(NewsId);
            var news = db.News.Where(x => x.Id == NewsId).FirstOrDefault();
            //найти старую главную картинку
            var OldMainPicture = AllPictures.Where(x => x.IsMain == true).FirstOrDefault();
            //сохраняем мссылку на главную картинку в новости
            news.MainPicturePath = MyPicture.FilePath;
            //сохранение
            db.SaveChanges();
            return MyPicture.Id;
        }
    }
}
