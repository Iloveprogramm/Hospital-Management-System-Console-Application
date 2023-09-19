﻿using HospitalManagementSystem;
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
}