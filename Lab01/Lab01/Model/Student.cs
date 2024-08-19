using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public override string ToString()
        {
            return $"Student ID: {StudentId} - Student Name: {StudentName}";
        }
    }
}
