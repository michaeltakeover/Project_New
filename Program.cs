using System;
using System.Linq; // Added for convenience
using System.Text.RegularExpressions; // For better validation

namespace ContactBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactBook contactBook = new ContactBook();

            // --- ADD DEFAULT CONTACTS ---
            Contact[] defaultContacts = 
            {
                new Contact("Osayande","Michael","Qyand Summit","811234567","osayande.michael@qse.ng", new DateTime(1992,2,29)),
                new Contact("Igbinoba","Joyce","BrightTech","811234568","igbinoba.joyce@bright.ng", new DateTime(1993,3,15)),
                new Contact("Eweka","Emmanuel","Eweka Ventures","811234569","eweka.emmanuel@eve.ng", new DateTime(1991,7,8)),
                new Contact("Okoro","Esther","Okoro Ltd","811234570","okoro.esther@okoro.ng", new DateTime(1990,6,10)),
                new Contact("Omoregie","David","Omoregie Co","811234571","omoregie.david@omr.ng", new DateTime(1989,12,12)),
                new Contact("Iruobe","Patricia","Iruobe Tech","811234572","iruobe.patricia@iru.ng", new DateTime(1994,1,5)),
                new Contact("Ugbome","Joseph","Ugbome Corp","811234573","ugbome.joseph@ugb.ng", new DateTime(1992,4,22)),
                new Contact("Aigbavboa","Rita","Aigbavboa Ltd","811234574","aigbavboa.rita@aig.ng", new DateTime(1991,9,18)),
                new Contact("Eghosa","Samuel","Eghosa Enterprises","811234575","eghosa.samuel@egh.ng", new DateTime(1993,11,30)),
                new Contact("Idahosa","Mercy","Idahosa Solutions","811234576","idahosa.mercy@ida.ng", new DateTime(1990,2,14)),
                new Contact("Asemota","Peter","Asemota Ventures","811234577","asemota.peter@ase.ng", new DateTime(1988,5,21)),
                new Contact("Igbinovia","Linda","Igbinovia Ltd","811234578","igbinovia.linda@igb.ng", new DateTime(1995,7,7)),
                new Contact("Egharevba","Daniel","Egharevba Co","811234579","egharevba.daniel@egh.ng", new DateTime(1991,3,3)),
                new Contact("Ihionkhan","Susan","Ihionkhan Tech","811234580","ihionkhan.susan@ihi.ng", new DateTime(1992,8,8)),
                new Contact("Omoregie","Rebecca","Omoregie Corp","811234581","omoregie.rebecca@omr.ng", new DateTime(1994,10,10)),
                new Contact("Rahul","Sharma","Sharma Ltd","911234582","rahul.sharma@sharma.in", new DateTime(1991,5,5)),
                new Contact("Ananya","Gupta","Gupta Corp","911234583","ananya.gupta@gupta.in", new DateTime(1994,7,7)),
                new Contact("Vikram","Singh","Singh Solutions","911234584","vikram.singh@singh.in", new DateTime(1990,9,9)),
                new Contact("Priya","Kaur","Kaur Enterprises","911234585","priya.kaur@kaur.in", new DateTime(1992,12,12)),
                new Contact("Arjun","Mehta","Mehta Tech","911234586","arjun.mehta@mehta.in", new DateTime(1993,3,3))
            };

            foreach(var c in defaultContacts)
                contactBook.AddContact(c);

            int choice;
            do
            {
                Console.WriteLine("\n===== CONTACT BOOK MENU =====");
                Console.WriteLine("1: Add Contact");
                Console.WriteLine("2: Show All Contacts");
                Console.WriteLine("3: Show Contact Details");
                Console.WriteLine("4: Update Contact");
                Console.WriteLine("5: Delete Contact");
                Console.WriteLine("0: Exit");
                Console.Write("Enter your choice: ");

                if(!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("❌ Invalid input! Please enter a number.");
                    continue;
                }

                switch(choice)
                {
                    case 1: AddContact(contactBook); break;
                    case 2: contactBook.ShowAllContacts(); break;
                    case 3: ShowContactDetails(contactBook); break;
                    case 4: UpdateContactMenu(contactBook); break;
                    case 5: DeleteContactMenu(contactBook); break;
                    case 0: Console.WriteLine("Exiting program..."); break;
                    default: Console.WriteLine("❌ Invalid option. Try again."); break;
                }

            } while(choice != 0);
        }

        // --- MENU ACTION METHODS ---

        static void AddContact(ContactBook contactBook)
        {
            Console.WriteLine("\n--- Add Contact ---");
            Console.Write("First Name: ");
            // Use ?? string.Empty to safely handle null input
            string firstName = Console.ReadLine() ?? string.Empty; 
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine() ?? string.Empty;
            Console.Write("Company: ");
            string company = Console.ReadLine() ?? string.Empty;
            
            // Re-prompting for valid input using helper methods
            string mobileNumber = GetValidInput("Mobile Number (e.g., 081234567): ", GetValidMobileNumber);
            string email = GetValidInput("Email: ", GetValidEmail);
            DateTime birthdate = GetValidDate();

            Contact newContact = new Contact(firstName, lastName, company, mobileNumber, email, birthdate);
            contactBook.AddContact(newContact);
        }

        static void ShowContactDetails(ContactBook contactBook)
        {
            contactBook.ShowAllContacts();
            if (contactBook.Count == 0) return;
            
            Console.Write("Enter contact number to view details: ");
            if(!int.TryParse(Console.ReadLine(), out int index) || index <= 0)
            {
                Console.WriteLine("❌ Invalid input.");
                return;
            }

            int zeroBasedIndex = index - 1;
            Contact? c = contactBook.GetContact(zeroBasedIndex);
            
            if(c == null)
            {
                Console.WriteLine("❌ Contact not found.");
                return;
            }

            Console.WriteLine("\n--- Contact Details ---");
            Console.WriteLine($"Name: {c.FirstName} {c.LastName}");
            Console.WriteLine($"Company: {c.Company}");
            Console.WriteLine($"Mobile: {c.MobileNumber}");
            Console.WriteLine($"Email: {c.Email}");
            Console.WriteLine($"Birthdate: {c.Birthdate:dd-MM-yyyy}");
        }

        static void DeleteContactMenu(ContactBook contactBook)
        {
            contactBook.ShowAllContacts();
            if (contactBook.Count == 0) return;
            
            Console.Write("Enter contact number to delete: ");
            if(!int.TryParse(Console.ReadLine(), out int index) || index <= 0)
            {
                Console.WriteLine("❌ Invalid input.");
                return;
            }

            int zeroBasedIndex = index - 1;
            contactBook.DeleteContact(zeroBasedIndex);
        }

        static void UpdateContactMenu(ContactBook contactBook)
        {
            Console.WriteLine("\n--- Update Contact ---");
            contactBook.ShowAllContacts(); 
            if (contactBook.Count == 0) return;

            Console.Write("Enter contact number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index <= 0)
            {
                Console.WriteLine("❌ Invalid input.");
                return;
            }

            int zeroBasedIndex = index - 1;
            Contact? contactToUpdate = contactBook.GetContact(zeroBasedIndex);
            if (contactToUpdate == null)
            {
                Console.WriteLine("❌ Contact not found.");
                return;
            }

            Console.WriteLine($"\nEditing Contact: {contactToUpdate.FirstName} {contactToUpdate.LastName}");
            Console.WriteLine("Enter new value, or press Enter to keep the current value.");

            // Fields are updated only if new non-blank data is provided
            
            Console.Write($"First Name (Current: {contactToUpdate.FirstName}): ");
            string? newFirstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newFirstName)) contactToUpdate.FirstName = newFirstName;

            Console.Write($"Last Name (Current: {contactToUpdate.LastName}): ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName)) contactToUpdate.LastName = newLastName;

            Console.Write($"Company (Current: {contactToUpdate.Company}): ");
            string? newCompany = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCompany)) contactToUpdate.Company = newCompany;
            
            // Prompt for mobile, but only apply if the new input is valid OR blank (to skip)
            Console.Write($"Mobile (Current: {contactToUpdate.MobileNumber}): ");
            string? newMobile = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newMobile)) 
            {
                if (ValidateMobileNumber(newMobile))
                    contactToUpdate.MobileNumber = newMobile;
                else 
                    Console.WriteLine("⚠️ Invalid format. Mobile number was not updated.");
            }

            // Prompt for email
            Console.Write($"Email (Current: {contactToUpdate.Email}): ");
            string? newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) 
            {
                if (ValidateEmail(newEmail))
                    contactToUpdate.Email = newEmail;
                else
                    Console.WriteLine("⚠️ Invalid format. Email was not updated.");
            }
            
            // Date Update 
            Console.Write($"Birthdate (Current: {contactToUpdate.Birthdate:dd-MM-yyyy}. Enter new date or press Enter to keep current): ");
            string? newDateString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDateString) && DateTime.TryParse(newDateString, out DateTime newDate))
            {
                 contactToUpdate.Birthdate = newDate;
            }
            else if (!string.IsNullOrWhiteSpace(newDateString))
            {
                Console.WriteLine("⚠️ Invalid date format, birthdate was not updated.");
            }

            // The object is modified by reference, but we call update for consistency
            contactBook.UpdateContact(zeroBasedIndex, contactToUpdate);
        }
        
        // --- VALIDATION HELPER METHODS ---
        
        // Generic wrapper for repeating validation prompts
        static string GetValidInput(string prompt, Func<string?, bool> validator)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (validator(input))
                {
                    // Use ?? string.Empty just in case, though validator should handle null/empty
                    return input ?? string.Empty; 
                }
                Console.WriteLine("❌ Invalid format. Please try again.");
            } while (true);
        }

        // Specific Mobile Validation Logic
        static bool ValidateMobileNumber(string? mobile)
        {
            // Simple validation: 9 digits, non-zero start.
            // Using Regex for robustness is better, but keeping simple check for C# console context
            return !string.IsNullOrWhiteSpace(mobile) && mobile.Length == 9 && long.TryParse(mobile, out long num) && num > 0;
        }

        // GetValidMobileNumber helper adapted for GetValidInput
        static bool GetValidMobileNumber(string? mobile) => ValidateMobileNumber(mobile);

        // Specific Email Validation Logic (Using Regex is best practice for C#)
        static bool ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            // Basic Regex check for 'A@B.C' structure
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }
        
        // GetValidEmail helper adapted for GetValidInput
        static bool GetValidEmail(string? email) => ValidateEmail(email);

        static DateTime GetValidDate()
        {
            while(true)
            {
                Console.Write("Birthdate (dd-MM-yyyy): ");
                if(DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;

                Console.WriteLine("❌ Invalid date. Try again.");
            }
        }
    }
}