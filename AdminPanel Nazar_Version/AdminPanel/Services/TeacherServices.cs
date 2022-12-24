﻿using AdminPanel.DbModels;
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
            return db.Teachers.ToList();
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

        public int Create(Teachers teachers)
        {
            db.Teachers.Add(teachers);
            db.SaveChanges();
            return teachers.Id;
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
