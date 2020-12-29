using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Employee.Entities;

namespace Employee {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            Console.Write("Enter salary: ");
            double salaryCompare = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);

            List<Employees> list = new List<Employees>();

            using (StreamReader sr = File.OpenText(path)) {
                while(!sr.EndOfStream) {
                    
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);

                    list.Add(new Employees(name, email, salary));
                }
            }

            var emails = list.Where(p => p.Salary > salaryCompare).OrderBy(p => p.Email).Select(p => p.Email);
            var sum = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);

            Console.WriteLine($"Email of people whose salary is more than {salaryCompare.ToString("F2",CultureInfo.InvariantCulture)}:");

            foreach(string email in emails) {
                Console.WriteLine(email);
            }

            Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2",CultureInfo.InvariantCulture));

        }
    }
}
