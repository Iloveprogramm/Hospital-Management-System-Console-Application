using HospitalManagementSystem;
using HospitalManagementSystem;

public class Doctor : User
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
            Console.WriteLine("\t\t\t |                 Doctor Menu                           |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t Welcome to DOTNET Hospital Management System");

            Console.WriteLine("\t\t\t  1. List doctor details                                 ");
            Console.WriteLine("\t\t\t  2. List patients                                       ");
            Console.WriteLine("\t\t\t  3. List appointments                                   ");
            Console.WriteLine("\t\t\t  4. Check particular patient                            ");
            Console.WriteLine("\t\t\t  7. Logout                                              ");
            Console.WriteLine("\t\t\t  8. Exit                                                ");
            Console.Write("\n\t\t\t Please select an option (1-8): ");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    DisplayDoctorDetails();
                    break;
                case "2":
                    ListPatients();
                    break; // Exit to
                case "3":
                    ListAppointments();
                    break; // Exit to login
                case "4":
                    CheckPatientDetails();
                    break; // Exit to login
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

    public void DisplayDoctorDetails()
    {
        Console.Clear();
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |                 My Details                              |");
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine($"\t\t\t Doctor ID: {ID}");
        Console.WriteLine($"\t\t\t Name: {FirstName} {LastName}");
        Console.WriteLine($"\t\t\t Email: {Email}");
        Console.WriteLine($"\t\t\t Phone: {Phone}");
        Console.WriteLine($"\t\t\t Address: {StreetNumber} {Street}, {City}, {State}");
    }

    public void ListAppointments()
    {
        Console.Clear();
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |                 Appointments Details                   |");
        Console.WriteLine("\t\t\t ===========================================================");

       
        if (File.Exists("appointment.txt"))
        {
            string[] appointments = File.ReadAllLines("appointment.txt");

            foreach (var appointment in appointments)
            {
                var parts = appointment.Split(',');
                if (parts.Length < 3) continue;

                string patientName = parts[0];
                string doctorName = parts[1];
                string description = parts[2];

               
                if (doctorName == $"{FirstName} {LastName}")
                {
                    Console.WriteLine($"\t\t\t Doctor: {doctorName}");
                    Console.WriteLine($"\t\t\t Patient: {patientName}");
                    Console.WriteLine($"\t\t\t Description: {description}");
                    Console.WriteLine("\t\t\t -----------------------------------------------------------");
                }
            }
        }
        else
        {
            Console.WriteLine("\t\t\t No appointments found!");
        }
    }


    public void ListPatients()
    {
        Console.Clear();
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine($"\t\t\t | Patients related to Dr. {FirstName} {LastName} ");
        Console.WriteLine("\t\t\t ===========================================================");

        var patientNames = GetPatientNamesFromAppointments($"{FirstName} {LastName}");
        foreach (var name in patientNames)
        {
            var patientDetails = GetPatientDetailsByName(name);
            if (patientDetails != null)
            {
                Console.WriteLine($"\t\t\t Name: {patientDetails.FirstName} {patientDetails.LastName}");
                Console.WriteLine($"\t\t\t Email: {patientDetails.Email}");
                Console.WriteLine($"\t\t\t Phone: {patientDetails.Phone}");
                Console.WriteLine($"\t\t\t Address: {patientDetails.StreetNumber} {patientDetails.Street}, {patientDetails.City}, {patientDetails.State}");
                Console.WriteLine("\t\t\t -----------------------------------------------------------");
            }
        }
    }

    public void CheckPatientDetails()
    {
        Console.Clear();
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |       DOTNET Hospital Management System                 |");
        Console.WriteLine("\t\t\t ===========================================================");
        Console.WriteLine("\t\t\t |                 Check Patient Details                   |");
        Console.WriteLine("\t\t\t ===========================================================");

        Console.Write("\t\t\t Enter the ID of the patient to check: ");
        string patientID = Console.ReadLine();
        string patientName = null;

        Dictionary<string, List<string>> doctorSymptoms = new Dictionary<string, List<string>>();

        
        if (File.Exists("patients.txt"))
        {
            string[] patients = File.ReadAllLines("patients.txt");

            foreach (var patient in patients)
            {
                var parts = patient.Split(',');
                if (parts.Length < 10) continue;

                if (parts[0] == patientID)
                {
                    patientName = parts[2] + " " + parts[3];  
                    Console.WriteLine("\t\t\t Patient Name: " + patientName);
                    Console.WriteLine("\t\t\t Email: " + parts[4]);
                    Console.WriteLine("\t\t\t Phone: " + parts[5]);
                    Console.WriteLine("\t\t\t Address: " + parts[6] + " " + parts[7] + ", " + parts[8] + ", " + parts[9]);
                    Console.WriteLine("\t\t\t -----------------------------------------------------------");
                    break;
                }
            }

            if (patientName == null)
            {
                Console.WriteLine("\t\t\t No patient found with the provided ID!");
                return;
            }

            
            if (File.Exists("appointment.txt"))
            {
                string[] appointments = File.ReadAllLines("appointment.txt");

                foreach (var appointment in appointments)
                {
                    var appointmentParts = appointment.Split(',');
                    if (appointmentParts.Length < 3) continue;

                    
                    if (appointmentParts[0] == patientName)
                    {
                        if (!doctorSymptoms.ContainsKey(appointmentParts[1]))
                        {
                            doctorSymptoms[appointmentParts[1]] = new List<string>();
                        }
                        doctorSymptoms[appointmentParts[1]].Add(appointmentParts[2]);
                    }
                }
            }

            
            foreach (var entry in doctorSymptoms)
            {
                Console.WriteLine("\t\t\t Doctor: " + entry.Key);
                Console.WriteLine("\t\t\t Symptoms: " + string.Join(", ", entry.Value));
                Console.WriteLine("\t\t\t -----------------------------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("\t\t\t No patient records found!");
        }
    }






    public List<string> GetPatientNamesFromAppointments(string doctorName)
    {
        List<string> patientNames = new List<string>();

        if (File.Exists("appointment.txt"))
        {
            foreach (var line in File.ReadAllLines("appointment.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 2 && parts[1] == doctorName)  // Assuming patientName is the first column and doctorName is the second column
                {
                    patientNames.Add(parts[0]);  // Adding patient's name to the list
                }
            }
        }

        return patientNames;
    }

    public Patient GetPatientDetailsByName(string patientName)
    {
        if (File.Exists("patients.txt"))
        {
            foreach (var line in File.ReadAllLines("patients.txt"))
            {
                var parts = line.Split(',');
                if (parts.Length >= 3 && $"{parts[2]} {parts[3]}" == patientName)  // Assuming firstName is at position 2 and lastName is at position 3
                {
                    return new Patient
                    {
                        FirstName = parts[2],
                        LastName = parts[3],
                        Email = parts[4],
                        Phone = parts[5],
                        StreetNumber = parts[6],
                        Street = parts[7],
                        City = parts[8],
                        State = parts[9]
                    };
                }
            }
        }

        return null;
    }


}