using Newtonsoft.Json;
using StudentTaskClient.Models;

namespace StudentTaskClient
{
    internal class Program
    {
        public static string Root = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose below option : \n 1. Add student. \n 2. Remove student. \n 3. Edit student. \n 4. Exit.");
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:

                        Console.WriteLine("Enter name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter surname: ");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Enter code: ");
                        string code = Console.ReadLine();

                        if (AddStudent(new Student { Name = name, Surname = surname, Code = code }))
                        {
                            Console.WriteLine("Succeed");
                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }

                        break;
                    case 2:

                        Console.WriteLine("Input code: ");
                        string code1 = Console.ReadLine();
                        RemoveStudent(code1);

                        break;
                    case 3:

                        Console.WriteLine("Input code: ");
                        string id = Console.ReadLine();
                        EditStudent(id);

                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input: ");
                        break;
                }
            }
        }

        static bool AddStudent(Student student)
        {
            List<Student> students = new List<Student>();

            using (StreamReader sr = new StreamReader(Path.Combine(Root, "students.json")))
            {
                string stds = sr.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(stds);
            }
            students.Add(student);

            using (StreamWriter sw = new StreamWriter(Path.Combine(Root, "students.json")))
            {
                string stds = JsonConvert.SerializeObject(students);
                sw.WriteLine(stds);
            }

            return true;
        }

        static void RemoveStudent(string code)
        {
            List<Student> students = new List<Student>();
            using (StreamReader sr = new StreamReader(Path.Combine(Root, "students.json")))
            {
                string stds = sr.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(stds);

                foreach (var item in students)
                {
                    if (item.Code == code)
                    {
                        students.Remove(item);
                        break;
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(Path.Combine(Root, "students.json")))
            {
                string stds = JsonConvert.SerializeObject(students);
                sw.WriteLine(stds);
            }
        }

        static void EditStudent(string code)
        {
            List<Student> students = new List<Student>();
            using (StreamReader sr = new StreamReader(Path.Combine(Root, "students.json")))
            {
                string stds = sr.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(stds);

                foreach (var item in students)
                {
                    if (item.Code == code)
                    {
                        Console.WriteLine("1. Edit name. \n2. Edit surname.");
                        int choose = Convert.ToInt32(Console.ReadLine());
                        switch (choose)
                        {
                            case 1:
                                Console.WriteLine("Name: ");
                                item.Name = Console.ReadLine();
                                break;
                            case 2:
                                Console.WriteLine("Surname: ");
                                item.Surname = Console.ReadLine();
                                break;
                        }
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(Path.Combine(Root, "students.json")))
            {
                string stds = JsonConvert.SerializeObject(students);
                sw.WriteLine(stds);
            }
        }
    }
}