using Lab01.Delegate;
using Lab01.Model;
using Lab01.Services;
using System;
using System.Collections.Generic;

namespace Lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            //load data from JSON files
            List<Student> students = ListToJson.LoadFromFile<Student>("Students.json");
            List<Teacher> teachers = ListToJson.LoadFromFile<Teacher>("Teachers.json");
            List<Courses> courses = ListToJson.LoadFromFile<Courses>("Courses.json");

            //subscribe to events
            foreach (var course in courses)
            {
                course.onCourseFullNoti += ReturnFullFunction.CourseFullReminder;
                course.onTeacherFullNoti += ReturnFullFunction.TeacherFullReminder;
            }

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Add Student to Course");
                Console.WriteLine("2. Remove Student from Course");
                Console.WriteLine("3. Assign Teacher to Course");
                Console.WriteLine("4. Edit Teacher in a Course");
                Console.WriteLine("5. Load Json");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine(); // Dấu ? sau kiểu string cho biết biến choice có thể là một chuỗi hợp lệ hoặc null

                switch (choice)
                {
                    case "1":
                        AddStudentToCourse(students, courses);
                        break;
                    case "2":
                        RemoveStudentFromCourse(courses);
                        break;
                    case "3":
                        AssignTeacherToCourse(teachers, courses);
                        break;
                    case "4":
                        EditTeacherInCourse(teachers, courses);
                        break;
                    case "5":
                        LoadJson();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }
        }

        static void AddStudentToCourse(List<Student> students, List<Courses> courses)
        {
            try
            {
                int studentId;
                while (true)
                {
                    Console.Write("Enter Student ID to add: ");
                    if (int.TryParse(Console.ReadLine(), out studentId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric Student ID.");
                    }
                }

                Console.WriteLine("Available Courses:");
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Code}: {course.Title}");
                }

                Courses? selectedCourse = null;
                while (selectedCourse == null)
                {
                    Console.Write("Enter the Course Code to add the student to: ");
                    string? courseCode = Console.ReadLine();

                    selectedCourse = courses.Find(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

                    if (selectedCourse == null)
                    {
                        Console.WriteLine("Invalid Course Code. Please try again.");
                    }
                }

                selectedCourse.AddStudentByID(studentId, students);
                ListToJson.SaveToFile(selectedCourse.Code + "Course.json", selectedCourse);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static void RemoveStudentFromCourse(List<Courses> courses)
        {
            try
            {
                Console.WriteLine("Available Courses:");
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Code}: {course.Title}");
                }

                Courses? selectedCourse = null;
                while (selectedCourse == null)
                {
                    Console.Write("Enter the Course Code to remove the student from: ");
                    string? courseCode = Console.ReadLine();

                    selectedCourse = courses.Find(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

                    if (selectedCourse == null)
                    {
                        Console.WriteLine("Invalid Course Code. Please try again.");
                    }
                }

                Console.WriteLine("Students in the course:");
                foreach (var student in selectedCourse.Students)
                {
                    Console.WriteLine($"ID: {student.StudentId}, Name: {student.StudentName}");
                }

                int studentId;
                while (true)
                {
                    Console.Write("Enter Student ID to remove: ");
                    if (int.TryParse(Console.ReadLine(), out studentId))
                    {
                        var student = selectedCourse.Students.Find(s => s.StudentId == studentId);
                        if (student != null)
                        {
                            selectedCourse.RemoveStudentByID(studentId);
                            ListToJson.SaveToFile(selectedCourse.Code + "Course.json", selectedCourse);
                            Console.WriteLine($"Student ID {studentId} has been removed.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Student ID not found. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric Student ID.");
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        static void AssignTeacherToCourse(List<Teacher> teachers, List<Courses> courses)
        {
            try
            {
                int teacherId;
                while (true)
                {
                    Console.Write("Enter Teacher ID to assign: ");
                    if (int.TryParse(Console.ReadLine(), out teacherId))
                    {
                        var teacher = teachers.Find(t => t.TeacherId == teacherId);
                        if (teacher != null)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Teacher ID not found. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric Teacher ID.");
                    }
                }

                Courses? selectedCourse = null;
                while (selectedCourse == null)
                {
                    Console.WriteLine("Available Courses:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"{course.Code}: {course.Title}");
                    }

                    Console.Write("Enter the Course Code to assign the teacher to: ");
                    string? courseCode = Console.ReadLine();
                    selectedCourse = courses.Find(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

                    if (selectedCourse == null)
                    {
                        Console.WriteLine("Invalid Course Code. Please try again.");
                    }
                }

                selectedCourse.AddTeacherByID(teacherId, teachers, courses);
                ListToJson.SaveToFile(selectedCourse.Code + "Course.json", selectedCourse);
                Console.WriteLine($"Teacher ID {teacherId} has been assigned to Course {selectedCourse.Code}.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static void EditTeacherInCourse(List<Teacher> teachers, List<Courses> courses)
        {
            try
            {
                Courses? selectedCourse = null;
                while (selectedCourse == null)
                {
                    Console.WriteLine("Available Courses:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"{course.Code}: {course.Title}");
                    }

                    Console.Write("Enter the Course Code to edit the teacher: ");
                    string? courseCode = Console.ReadLine();
                    selectedCourse = courses.Find(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

                    if (selectedCourse == null)
                    {
                        Console.WriteLine("Invalid Course Code. Please try again.");
                    }
                }

                Console.WriteLine("Current Teacher:");
                if (selectedCourse.Teacher != null)
                {
                    Console.WriteLine($"ID: {selectedCourse.Teacher.TeacherId}, Name: {selectedCourse.Teacher.TeacherName}");
                }
                else
                {
                    Console.WriteLine("No teacher assigned to this course.");
                }

                Teacher? newTeacher = null;
                while (newTeacher == null)
                {
                    Console.Write("Enter New Teacher ID: ");
                    if (int.TryParse(Console.ReadLine(), out int newTeacherId))
                    {
                        newTeacher = teachers.Find(t => t.TeacherId == newTeacherId);

                        if (newTeacher == null)
                        {
                            Console.WriteLine("Invalid Teacher ID. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric Teacher ID.");
                    }
                }

                selectedCourse.EditTeacher(newTeacher);
                ListToJson.SaveToFile(selectedCourse.Code + "Course.json", selectedCourse);
                Console.WriteLine($"Teacher ID {newTeacher.TeacherId} has been assigned to Course {selectedCourse.Code}.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void LoadJson()
        {
            try
            {
                string[] files = Directory.GetFiles(".", "*.json");

                if (files.Length == 0)
                {
                    Console.WriteLine("No JSON files found.");
                    return;
                }

                Console.WriteLine("Available JSON files:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }

                Console.Write("Enter the number of the file to view: ");
                if (int.TryParse(Console.ReadLine(), out int fileIndex) && fileIndex > 0 && fileIndex <= files.Length)
                {
                    string filePath = files[fileIndex - 1];
                    string jsonContent = File.ReadAllText(filePath);
                    Console.WriteLine($"\nContents of {Path.GetFileName(filePath)}:");
                    Console.WriteLine(jsonContent);
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



    }
}
