using AdminPanel.Models;
using AdminPanel.Models;

namespace AdminPanel.Services
{
    public class TeacherService
    {
        ApplicationContext db;
        public TeacherService(ApplicationContext context)
        {
            db = context;
        }
        public List<Teachers> GetAll()
        {
            var l = db.Teachers.ToList();
            return l;
        }
        public Teachers GetDetails(int TeacherId)
        {
            return db.Teachers.Where(x => x.Id == TeacherId).FirstOrDefault();
        }
        public void DeleteTeachers(Teachers teachers)
        {
            db.Teachers.Remove(teachers);
            db.SaveChanges();
        }
        public int Edit(Teachers teacher)
        {
            //получить новость с таким же ID из базы. 
            var OldNews = GetDetails(teacher.Id);
            //Обновляю все поля из новых полей которые пришли
            db.Entry(OldNews).CurrentValues.SetValues(teacher);
            //сохраняю все в базе
            db.SaveChanges();
            //вернуть айди
            return teacher.Id;
        }
        public int Create(Teachers teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return teacher.Id;
        }

    }
}
