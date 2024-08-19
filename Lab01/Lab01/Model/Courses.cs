using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab01.Delegate;

namespace Lab01.Model
{
    public class Courses
    {
        public string Code { get; set; } = string.Empty; // Hoặc giá trị mặc định khác
        public string Title { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; } //danh dau thuoc tinh la nullable
        public List<Student> Students { get; set; } = new List<Student>();

        private CourseFullNoti? courseFullNoti;
        private TeacherFullNoti? teacherFullNoti;

        public event CourseFullNoti onCourseFullNoti
        {
            add { courseFullNoti += value; }
            remove { courseFullNoti -= value; }
        }

        public event TeacherFullNoti onTeacherFullNoti
        {
            add { teacherFullNoti += value; }
            remove { teacherFullNoti -= value; }
        }

        //add student function
        public void AddStudentByID(int studentId, List<Student> students)
        {
            // Attempt to find the student by ID
            Student? student = students.FirstOrDefault(s => s.StudentId == studentId);

            // Check if the student was found
            if (student == null)
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
                return; // Exit if the student is not found
            }

            // Check if the course is full
            if (Students.Count >= 5)
            {
                // Safely invoke the delegate, if it's not null
                courseFullNoti?.Invoke();
            }
            else
            {
                // Add the student to the course's list of students
                Students.Add(student);
                Console.WriteLine($"Student {student.StudentName} added to the course.");
            }
        }


        //remove student function
        public void RemoveStudentByID(int studentId)
        {
            // Attempt to find the student by ID
            Student? student = Students.FirstOrDefault(s => s.StudentId == studentId);

            // Check if the student was found
            if (student == null)
            {
                Console.WriteLine($"Student with ID {studentId} not found in the course.");
                return; // Exit if the student is not found
            }

            // Remove the student from the course's list of students
            bool removed = Students.Remove(student);

            // Provide feedback based on whether the removal was successful
            if (removed)
            {
                Console.WriteLine($"Student {student.StudentName} removed from the course.");
            }
            else
            {
                Console.WriteLine($"Failed to remove Student {student.StudentName} from the course.");
            }
        }


        public void AddTeacherByID(int teacherId, List<Teacher> teachers, List<Courses> courses)
        {
            // Find the teacher with the specified ID
            Teacher? teacher = teachers.FirstOrDefault(t => t.TeacherId == teacherId);

            // Check if the teacher was found
            if (teacher == null)
            {
                Console.WriteLine($"Teacher with ID {teacherId} not found.");
                return; // Exit the method if teacher is not found
            }

            // Count the number of courses that the teacher is currently assigned to
            int teacherCourseCount = courses.Count(c => c.Teacher != null && c.Teacher.TeacherId == teacherId);

            // Notify if the teacher is already assigned to 2 or more courses
            if (teacherCourseCount >= 2)
            {
                teacherFullNoti?.Invoke(); // Safe call to the delegate
                Console.WriteLine($"Teacher {teacher.TeacherName} is already assigned to {teacherCourseCount} courses.");
            }
            else
            {
                // Assign the teacher to the course
                Teacher = teacher; // Ensure that Teacher is a property of the class
                Console.WriteLine($"Teacher {teacher.TeacherName} assigned to the course.");
            }
        }


        //edit teacher
        public void EditTeacher(Teacher teacher)
        {
            Teacher = teacher;
            Console.WriteLine($"Teacher {teacher.TeacherName} Changed to for course {Title}");
        }
    }
}
