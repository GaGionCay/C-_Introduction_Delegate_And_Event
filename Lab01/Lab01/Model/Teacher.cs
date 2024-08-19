using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public override string ToString()
        {
            return $"Teacher ID: {TeacherId} - Teacher Name: {TeacherName}";
        }
    }
}
