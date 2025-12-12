using System;

namespace ContactBookApp
{

    class Contact
    {
        // Properties (Encapsulation)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        // Parameterless constructor
        public Contact() 
        {
            // Initialize strings to prevent null reference warnings if properties are not set immediately
            FirstName = string.Empty;
            LastName = string.Empty;
            Company = string.Empty;
            MobileNumber = string.Empty;
            Email = string.Empty;
        }

        // Constructor with parameters
        public Contact(string firstName, string lastName, string company,
                       string mobileNumber, string email, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            MobileNumber = mobileNumber;
            Email = email;
            Birthdate = birthdate;
        }
    }
}