using Lab01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Services
{
    public class MainFunction
    {
        private List<Student> students;
        private List<Teacher> teachers;
        private List<Courses> courses;

        public MainFunction() 
        {
            students = ListToJson.LoadFromFile<Student>("Student.json");
            teachers = ListToJson.LoadFromFile<Teacher>("Teacher.json");
            courses = new List<Courses>();
        }
    }
}
