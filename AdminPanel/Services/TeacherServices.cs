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
        public int Create(Teachers teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            return teacher.Id;
        }
         public int Edit(Teachers teachers)
        {
            var oldId= db.Teachers.Where(x => x.Id == teachers.Id).FirstOrDefault();

            var Oldteacher = GetDetails(teachers.Id);
            db.Entry(Oldteacher).CurrentValues.SetValues(teachers);
            db.SaveChanges();
            return teachers.Id;
        }

    }
}
