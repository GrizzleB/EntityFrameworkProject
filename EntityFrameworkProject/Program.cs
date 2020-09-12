using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentContext())
            {
                var student = new Student
                {
                    FirstName = "Ryan",
                    LastName = "Gerhardt"
                };
                db.Students.Add(student);
                db.SaveChanges();

                var course = new Course
                {
                    Description = "C# III",
                    StartDate = Convert.ToDateTime("08/20/2020"),
                    EndDate = Convert.ToDateTime("10/20/2020"),
                    StudentId = student
                };
                db.Courses.Add(course);

                course = new Course
                {
                    Description = "General Psychology",
                    StartDate = Convert.ToDateTime("10/20/2020"),
                    EndDate = Convert.ToDateTime("12/20/2020"),
                    StudentId = student
                };
                db.Courses.Add(course);

                course = new Course
                {
                    Description = "Red Cross CPR",
                    StartDate = Convert.ToDateTime("08/20/2020"),
                    EndDate = Convert.ToDateTime("10/05/2020"),
                    StudentId = student
                };
                db.Courses.Add(course);
                db.SaveChanges();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }

        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Student StudentId { get; set; }

    }

    public class StudentContext : DbContext
    {
        public StudentContext()
        {

        }

        public StudentContext(string conn)
        {
            Database.Connection.ConnectionString = conn;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().MapToStoredProcedures(
                g => g.Insert(sp => sp.HasName("sp_InsertStudent"))
                      .Update(sp => sp.HasName("sp_UpdateStudent"))
                      .Delete(sp => sp.HasName("sp_DeleteStudent"))
                );
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

    }

}
