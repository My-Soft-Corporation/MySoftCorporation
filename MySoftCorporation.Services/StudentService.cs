﻿using MySoft.Institute.Entities;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class StudentService
    {
        public IEnumerable<Student> GetAll()
        {
            return new MySoftCorporationDbContext().Students.AsEnumerable();
        }

        public IEnumerable<Student> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var objectsAll = mySoftCorporationDbContext.Students.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                objectsAll = objectsAll.Where(a => a.FirstName.ToLower().Contains(SearchTerm.ToLower()));

            return objectsAll.AsEnumerable();
        }

        public Student GetByID(int ID)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                return context.Students.Find(ID);
            }
        }

        public bool Save(Student objectFirst)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Students.Add(objectFirst);
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(Student objectFirst)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Entry(objectFirst).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public bool Delete(Student objectFirst)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Entry(objectFirst).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }

        public Student GetStudentByUserId(string UserID)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var Students = mySoftCorporationDbContext.Students.AsQueryable();
            return Students.Where(a => a.UserID.Contains(UserID)).SingleOrDefault();
        }
    }
}