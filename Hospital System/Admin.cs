using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

namespace HospitalManagementSystem
{
    public class Admin : User
    {
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |       DOTNET Hospital Management System - ADMIN         |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |                 Administrator Menu                      |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t Welcome to DOTNET Hospital Management System");
                Console.WriteLine("\t\t\t  1. List all Doctors                                     ");
                Console.WriteLine("\t\t\t  2. Check Doctor Details                                 ");
                Console.WriteLine("\t\t\t  3. List all Patients                                    ");
                Console.WriteLine("\t\t\t  4. Check Patient Details                                ");
                Console.WriteLine("\t\t\t  5. Add doctor                                           ");
                Console.WriteLine("\t\t\t  6. Add patient                                          ");
                Console.WriteLine("\t\t\t  7. Logout                                               ");
                Console.WriteLine("\t\t\t  8. Exit                                                 ");

                Console.Write("\n\t\t\t Please select an option (1-8): ");

                // 获取当前行和列位置
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                // 将光标移到你希望的位置
                Console.SetCursorPosition(left, top);

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListDoctors();
                        break;
                    case "2":
                        CheckDoctorDetails();
                        break;
                    case "3":
                        ListPatients();
                        break;
                    case "5":
                        AddDoctor();
                        break;
                    case "6":
                        AddPatient();
                        break;
                    case "7":
                        return;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\t\t\t Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine("\t\t\t Press any key to continue...");
                Console.ReadKey(); // Wait for user to press a key before refreshing the menu
            }
        }

        public void CheckDoctorDetails()
        {
            if (!File.Exists("doctors.txt"))
            {
                Console.WriteLine("Doctors file not found!");
                return;
            }

            Console.Write("Please enter the ID of the doctor you want to view: ");
            string inputID = Console.ReadLine();

            var doctorLines = File.ReadAllLines("doctors.txt");

            foreach (var line in doctorLines)
            {
                var details = line.Split(',');
                if (details.Length != 10) // 注意现在是10个值
                {
                    Console.WriteLine($"Skipping malformed line: " + line);
                    continue;
                }

                string id = details[0].Trim();

                if (id == inputID)
                {
                    string name = "Name: " + details[2] + " " + details[3];  // 注意索引的变化
                    string email = "Email: " + details[4];
                    string phone = "Phone: " + details[5];
                    string address = "Address: " + details[6] + ", " + details[7] + ", " + details[8] + ", " + details[9];

                    Console.WriteLine(name);
                    Console.WriteLine(email);
                    Console.WriteLine(phone);
                    Console.WriteLine(address);
                    return;
                }
            }

            Console.WriteLine("Doctor with the given ID not found!");
        }

        public void CheckPatientsDetails()
        {
            if (!File.Exists("patients.txt"))
            {
                Console.WriteLine("Doctors file not found!");
                return;
            }

            Console.Write("Please enter the ID of the patients you want to view: ");
            string inputID = Console.ReadLine();

            var doctorLines = File.ReadAllLines("patients.txt");

            foreach (var line in doctorLines)
            {
                var details = line.Split(',');
                if (details.Length != 9)
                {
                    Console.WriteLine($"Skipping malformed line: " + line);
                    continue;
                }

                string id = details[0].Trim();

                if (id == inputID)
                {
                    string name = "Name: " + details[1] + " " + details[2];
                    string email = "Email: " + details[3];
                    string phone = "Phone: " + details[4];
                    string address = "Address: " + details[5] + ", " + details[6] + ", " + details[7] + ", " + details[8];

                    Console.WriteLine(name);
                    Console.WriteLine(email);
                    Console.WriteLine(phone);
                    Console.WriteLine(address);
                    return;
                }
            }

            Console.WriteLine("Patients with the given ID not found!");
        }




        public void AddPatient()
        {
            Patient patient = new Patient();

            Console.Write("Enter First Name: ");
            patient.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            patient.LastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            patient.Email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            patient.Phone = Console.ReadLine();

            Console.Write("Enter Street Number: ");
            patient.StreetNumber = Console.ReadLine();

            Console.Write("Enter Street: ");
            patient.Street = Console.ReadLine();

            Console.Write("Enter City: ");
            patient.City = Console.ReadLine();

            Console.Write("Enter State: ");
            patient.State = Console.ReadLine();

            patient.ID = GenerateID();
            patient.Password = GeneratePassword();
            Console.WriteLine($"{patient.LastName} added to the System!");
            File.AppendAllText("patients.txt", patient.ToString() + Environment.NewLine);
        }


        public void AddDoctor()
        {
            Doctor doctor = new Doctor();

            Console.Write("Enter First Name: ");
            doctor.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            doctor.LastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            doctor.Email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            doctor.Phone = Console.ReadLine();

            Console.Write("Enter Street Number: ");
            doctor.StreetNumber = Console.ReadLine();

            Console.Write("Enter Street: ");
            doctor.Street = Console.ReadLine();

            Console.Write("Enter City: ");
            doctor.City = Console.ReadLine();

            Console.Write("Enter State: ");
            doctor.State = Console.ReadLine();

            doctor.ID = GenerateDoctorID();
            doctor.Password = GeneratePassword();
            Console.WriteLine($"{doctor.LastName} added to the System!");
            File.AppendAllText("doctors.txt", doctor.ToString() + Environment.NewLine);
        }


        public void ListPatients()
        {
            if (!File.Exists("patients.txt"))
            {
                Console.WriteLine("Patients file not found!");
                return;
            }

            var patientLines = File.ReadAllLines("patients.txt");
            Console.WriteLine("Listing all patients:");
            Console.WriteLine(new string('=', 50));

            foreach (var line in patientLines)
            {
                var details = line.Split(',');
                if (details.Length < 8)
                {
                    Console.WriteLine("Skipping malformed line: {line}");
                    continue;
                }
                string name = "Name: " + details[2].Split(':')[1].Trim() + " " + details[3].Split(':')[1].Trim(); // FirstName + LastName
                string email = "Email: " + details[4].Split(':')[1].Trim();
                string phone = "Phone: " + details[5].Split(':')[1].Trim();
                string address = "Address: " + details[6].Split(':')[1].Trim(); // Address

                Console.WriteLine($"{name,-35} {email,-35}");
                Console.WriteLine($"{phone,-35} {address,-35}");
                Console.WriteLine(new string('-', 50));
            }
        }

        public void ListDoctors()
        {
            if (!File.Exists("doctors.txt"))
            {
                Console.WriteLine("Doctors file not found!");
                return;
            }

            // 清除屏幕并展示新的菜单
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System           |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                        All Doctors                      |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t All doctors registered to the DOTNET Hospital Management System".PadLeft(72));


            // 标题行
            Console.WriteLine($"\n\t\t\t {"Name",-20}| {"Email",-30}| {"Phone",-15}| {"Address",-40}");
            Console.WriteLine("\t\t\t ===============================================================================");

            var doctorLines = File.ReadAllLines("doctors.txt");

            foreach (var line in doctorLines)
            {
                var details = line.Split(',');
                if (details.Length < 8)
                {
                    Console.WriteLine($"Skipping malformed line: {line}");
                    continue;
                }

                string name = details[2].Trim() + " " + details[3].Trim();
                string email = details[4].Trim();
                string phone = details[5].Trim();
                string address = details[6].Trim() + ", " + details[7].Trim();

                // 打印每行信息
                Console.WriteLine($"\t\t\t {name,-20}| {email,-30}| {phone,-15}| {address,-40}");
            }


            Console.WriteLine("\n \t\t\t Press any key to return to the main menu...");
            Console.ReadKey();
        }




        private int GenerateID()
        {
            Random rnd = new Random();
            return rnd.Next(10000, 99999);
        }

        private int GenerateDoctorID()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999);  // 六位数的ID
        }


        private string GeneratePassword()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString();
        }
    }
}