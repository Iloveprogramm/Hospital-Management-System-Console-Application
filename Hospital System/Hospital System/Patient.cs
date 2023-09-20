using System;

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
            Console.Clear();
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |       DOTNET Hospital Management System - ADMIN         |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t |                 Patients Menu                           |");
            Console.WriteLine("\t\t\t ===========================================================");
            Console.WriteLine("\t\t\t Press any key to continue...");
            Console.ReadLine();
        }
    }
}
