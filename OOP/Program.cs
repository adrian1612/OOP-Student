using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student();
            int option = 0;
            do
            {
                Console.Clear();
                Console.Write("1. Add Student\n2. Display all student data\n3. Number of Males/Females\n4. Exit\n\nSelect Option: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        s.Add(new Student(true));
                        break;
                    case 2:
                        s.DisplayAll();
                        Console.ReadLine();
                        break;
                    case 3:
                        s.PrintStatisticGender();
                        Console.ReadLine();
                        break;
                    case 4:
                        option = 4;
                        break;
                }
            } while (option != 4);
        }
    }

    class Student : Tools
    {
        private int studentNo;

        public int StudentNo
        {
            get { return studentNo; }
            set { studentNo = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string degree;

        public string Degree
        {
            get { return degree; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    degree = "N/A";
                }
                else
                {
                    degree = value;
                }
            }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    gender = "N/A";
                }
                else
                {
                    gender = value;
                }
            }
        }

        private string emal;

        public string Email
        {
            get { return emal; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    emal = "N/A";
                }
                else
                {
                    emal = value;
                }
            }
        }

        public Student()
        {

        }

        public Student(bool insert)
        {
            Console.Write("Enter Student No: ");
            StudentNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name: ");
            Name = Console.ReadLine();

            Console.Write("Enter Degree: ");
            Degree = Console.ReadLine();

            Console.Write("Enter Gender: ");
            Gender = Console.ReadLine();

            Console.Write("Enter Email: ");
            Email = Console.ReadLine();
        }

        public List<Student> ListStudent()
        {
            students.Clear();
            using (var fs = new FileStream("student.txt", FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    var ln = sr.ReadToEnd().Split('\n', '\r');
                    foreach (string s in ln)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            var arr = s.Split(';');
                            students.Add(new Student
                            {
                                StudentNo = Convert.ToInt32(arr[0]),
                                Name = arr[1],
                                Degree = arr[2],
                                Gender = arr[3],
                                Email = arr[4]
                            });
                        }
                    }
                }
            }
            return students;
        }

        List<Student> students = new List<Student>();

        public int? NoMale { get { return students?.Count(f => f.Gender == "Male"); } }

        public int? NoFemale { get { return students?.Count(f => f.Gender == "Female"); } }

        public void PrintStatisticGender()
        {
            ListStudent();
            Console.WriteLine("Number of Male: {0}\nNumber of Female: {1}", NoMale, NoFemale);
            Console.WriteLine("\n\nPress any key to continue...");
        }

        public void DisplayAll()
        {
            PrintLine();
            PrintRow("Student No", "Name", "Degree", "Gender", "Email");
            ListStudent().ForEach(r =>
            {
                PrintLine();
                PrintRow(r.StudentNo.ToString(), r.Name, r.Degree, r.Gender, r.Email);
            });
            Console.WriteLine("\n\nPress any key to continue...");
        }

        public void Add(Student stud)
        {
            using (var fs = new FileStream("student.txt", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{stud.StudentNo};{stud.Name};{stud.Degree};{stud.Gender};{stud.Email}");
                }
            }
        }

   

    }

    class Tools
    {
        public Tools()
        {

        }

        public void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }


        int tableWidth = 79;
        public void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }

}
