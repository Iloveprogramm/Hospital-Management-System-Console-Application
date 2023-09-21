using System;
using System.IO;

namespace HospitalManagementSystem
{
    class Program
    {
        static Admin admin = new Admin();
        static Patient patient = new Patient();
        static Doctor doctor = new Doctor();

        static void Main(string[] args)
        {
            ShowLoginMenu();
        }

        static void ShowLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |     WELCOME TO DOTNET Hospital Management System        |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |                 PLEASE LOGIN TO START                   |");
                Console.WriteLine("\t\t\t |                                                         |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine();
                Console.Write("\t\t\t Enter User ID: ");
                string id = Console.ReadLine();

                Console.Write("\t\t\t Enter Password: ");
                string password = ReadPassword();

                string details;
                if ((details = IsValidCredentials(id, password, "patients.txt")) != null)
                {
                    Console.WriteLine("\t\t\t Login successful!");

                    var patientDetails = details.Split(',');
                    patient.ID = int.Parse(patientDetails[0]);
                    patient.Password = patientDetails[1];
                    patient.FirstName = patientDetails[2];
                    patient.LastName = patientDetails[3];
                    patient.Email = patientDetails[4];
                    patient.Phone = patientDetails[5];
                    patient.StreetNumber = patientDetails[6];
                    patient.Street = patientDetails[7];
                    patient.City = patientDetails[8];
                    patient.State = patientDetails[9];

                    patient.showMenu();
                }
                else if ((details = IsValidCredentials(id, password, "doctors.txt")) != null)
                {
                    Console.WriteLine("\t\t\t Login successful!");

                    var doctorDetails = details.Split(',');
                    doctor.ID = int.Parse(doctorDetails[0]);
                    doctor.Password = doctorDetails[1];
                    doctor.FirstName = doctorDetails[2];
                    doctor.LastName = doctorDetails[3];
                    doctor.Email = doctorDetails[4];
                    doctor.Phone = doctorDetails[5];
                    doctor.StreetNumber = doctorDetails[6];
                    doctor.Street = doctorDetails[7];
                    doctor.City = doctorDetails[8];
                    doctor.State = doctorDetails[9];

                    doctor.showMenu();
                }
                else if (IsValidCredentials(id, password, "admin.txt") != null)
                {
                    Console.WriteLine("\t\t\t Login successful!");
                    admin.ShowMenu();
                }
                else
                {
                    Console.WriteLine("\n\t\t\t Invalid credentials!");
                    Console.Write("\t\t\t Do you want to retry? (Y/N): ");
                    string choice = Console.ReadLine().ToUpper();

                    if (choice != "Y")
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        static string IsValidCredentials(string id, string password, string filePath)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length > 1 && parts[0] == id && parts[1] == password)
                    return line;  // Return the entire line of details
            }

            return null;
        }

        static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter) break;

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += key.KeyChar;
                }
            }
            return password;
        }

        public static List<Doctor> LoadDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            if (!File.Exists("doctors.txt")) return doctors;

            foreach (var line in File.ReadAllLines("doctors.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length < 10) continue;

                doctors.Add(new Doctor
                {
                    ID = int.Parse(parts[0]),
                    Password = parts[1],
                    FirstName = parts[2],
                    LastName = parts[3],
                    Email = parts[4],
                    Phone = parts[5],
                    StreetNumber = parts[6],
                    Street = parts[7],
                    City = parts[8],
                    State = parts[9]
                });
            }

            return doctors;
        }

        public static Doctor LoadDoctorByName(string doctorFullName)
        {
            var doctors = LoadDoctors();
            foreach (var doctor in doctors)
            {
                if (doctorFullName == $"{doctor.FirstName} {doctor.LastName}")
                {
                    return doctor;
                }
            }
            return null;
        }

        public static void SaveAppointment(string patientName, string doctorName, string description)
        {
            File.AppendAllText("appointment.txt", $"{patientName},{doctorName},{description}\n");
        }
    }

}
