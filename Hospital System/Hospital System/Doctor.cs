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
                    return; // Exit to login
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

}