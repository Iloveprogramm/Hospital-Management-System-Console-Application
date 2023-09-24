using System;
using System.IO;

namespace HospitalManagementSystem
{
    public class Patient : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public override string ToString()
        {
            return $"{ID},{Password},{FirstName},{LastName},{Email},{Phone},{StreetNumber},{Street},{City},{State}";
        }

        public void showMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |                 Patients Menu                           |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t Welcome to DOTNET Hospital Management System");

                Console.WriteLine("\t\t\t  1. List patient details                                 ");
                Console.WriteLine("\t\t\t  2. List my doctor details                               ");
                Console.WriteLine("\t\t\t  3. List all appointments                                ");
                Console.WriteLine("\t\t\t  4. Book appointment                                     ");
                Console.WriteLine("\t\t\t  7. Exit to login                                        ");
                Console.WriteLine("\t\t\t  8. Exit System                                          ");
                Console.Write("\n\t\t\t Please select an option (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        listPatientDetails();
                        break;
                    case "2":
                        ListMyDoctors();
                        break;
                    case "3":
                        ListAppointments();
                        break;
                    case "4":
                        BookAppointment();
                        break;
                    // case 5 and case 6 are missing
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
                Console.ReadKey();
            }
        }

        public void listPatientDetails()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                 My Details                              |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine($"\n\t\t\t {FirstName} {LastName}'s Details");
            Console.WriteLine($"\n\t\t\t ID: {ID}");
            Console.WriteLine($"\t\t\t Full Name: {FirstName} {LastName}");
            Console.WriteLine($"\t\t\t Email: {Email}");
            Console.WriteLine($"\t\t\t Phone: {Phone}");
            Console.WriteLine($"\t\t\t Address: {StreetNumber} {Street}, {City}, {State}");
        }

        public bool HasExistingAppointment(string patientName, string doctorName)
        {
            if (!File.Exists("appointment.txt")) return false;
            foreach (var line in File.ReadAllLines("appointment.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length < 3) continue;
                if (parts[0] == patientName && parts[1] == doctorName) return true;
            }
            return false;
        }

        public void BookAppointment()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                 Book Appointments                       |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t Choose a doctor from the list:");
            var doctors = Program.LoadDoctors();
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"\t\t\t {i + 1}. Dr. {doctors[i].FirstName} {doctors[i].LastName} ...");
            }

            int choice;
            while (true)
            {
                Console.Write("\t\t\t Please choose a doctor (1, 2, 3...): ");
                
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                
                Console.SetCursorPosition(left, top);
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= doctors.Count) break;
                Console.WriteLine("\t\t\t Invalid choice. Do you want to try again(Y/N)?");
                string re = Console.ReadLine().Trim().ToLower();
                if (re == "Y")
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t ===========================================================");
                    Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
                    Console.WriteLine("\t\t\t ===========================================================");
                    Console.WriteLine("\t\t\t |                 My Details                              |");
                    Console.WriteLine("\t\t\t ===========================================================");
                    BookAppointment(); // Recursive call to book with another doctor.
                    return;
                }
                else
                {
                    return;
                }

            }

            string doctorName = $"{doctors[choice - 1].FirstName} {doctors[choice - 1].LastName}";
            string patientName = $"{FirstName} {LastName}";

            if (HasExistingAppointment(patientName, doctorName))
            {
                Console.WriteLine($"\t\t\tYou already have an appointment with Dr. {doctorName}.");
                Console.Write("\t\t\tWould you like to book an appointment with another doctor? (Yes/No) ");


                string response = Console.ReadLine().Trim().ToLower();
                if (response == "yes")
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t ===========================================================");
                    Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
                    Console.WriteLine("\t\t\t ===========================================================");
                    Console.WriteLine("\t\t\t |                 My Details                              |");
                    Console.WriteLine("\t\t\t ===========================================================");
                    BookAppointment(); // Recursive call to book with another doctor.
                    return;
                }
                else
                {
                    return;
                }
            }

            Console.Write("\t\t\tPlease describe your condition or reason for appointment: ");
            string description = Console.ReadLine();

            Program.SaveAppointment(patientName, doctorName, description);
            Console.WriteLine($"\t\t\tSuccessfully booked an appointment with Dr. {doctorName}. Condition/Reason: {description}");
        }


        public void ListAppointments()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                 My  Appointments                        |");
            Console.WriteLine("\t\t\t ===========================================================");
        

            string appointmentsFile = "appointment.txt";

            if (!File.Exists(appointmentsFile))
            {
                Console.WriteLine("\t\t\t No appointments found.".PadLeft(55));
                return;
            }

            Console.WriteLine($"\n\t\t\t {"Patient Name",-20}| {"Doctor Name",-20}| {"Symptom",-30}");
            Console.WriteLine("\t\t\t ===============================================================================");

            string[] appointments = File.ReadAllLines(appointmentsFile);

            bool hasAppointment = false;
            foreach (var appointment in appointments)
            {
                var parts = appointment.Split(',');
                if (parts.Length < 3) continue;

                var patientName = parts[0];
                var doctorName = parts[1];
                var symptom = parts[2];

                if (patientName == FirstName + " " + LastName)
                {
                    hasAppointment = true;
                    Console.WriteLine($"\t\t\t {patientName,-20}| {doctorName,-20}| {symptom,-30}");
                    Console.WriteLine("\t\t\t -------------------------------------------------------------------------------");
                }
            }

            if (!hasAppointment)
            {
                Console.WriteLine("\t\t\t No appointments found for " + FirstName + " " + LastName + ".");
            }
        }



        public void ListMyDoctors()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                 My Doctors                              |");
            Console.WriteLine("\t\t\t ===========================================================");

            string appointmentsFile = "appointment.txt";
            if (!File.Exists(appointmentsFile))
            {
                Console.WriteLine("\t\t\t No appointments found.");
                return;
            }

            var doctorNames = new List<string>();
            string[] appointments = File.ReadAllLines(appointmentsFile);
            foreach (var appointment in appointments)
            {
                var parts = appointment.Split(',');
                if (parts.Length < 3) continue;

                var patientName = parts[0];
                var doctorName = parts[1];

                if (patientName == FirstName + " " + LastName)
                {
                    if (!doctorNames.Contains(doctorName))
                    {
                        doctorNames.Add(doctorName);
                    }
                }
            }

            foreach (var doctorName in doctorNames)
            {
                var doctor = Program.LoadDoctorByName(doctorName);
                if (doctor != null)
                {
                    Console.WriteLine($"\t\t\t Full Name: {doctor.FirstName} {doctor.LastName}");
                    Console.WriteLine($"\t\t\t Email: {doctor.Email}");
                    Console.WriteLine($"\t\t\t Phone: {doctor.Phone}");
                    Console.WriteLine($"\t\t\t Address: {doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}");
                    Console.WriteLine("\t\t\t -------------------------------------------------------");
                }
            }
        }

    }
}
