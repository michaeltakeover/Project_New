using System;
using System.Collections.Generic;

namespace ContactBookApp
{
    /// Manages the collection of Contact objects.
    class ContactBook
    {
        // Private list to store contacts, ensuring encapsulation.
        private readonly List<Contact> contacts = new List<Contact>();

        /// Adds a new contact to the book.
        public void AddContact(Contact newContact)
        {
            contacts.Add(newContact);
            Console.WriteLine("✅ Contact added successfully!");
        }

        // Displays a numbered list of all contacts' names.
        public void ShowAllContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("🚫 No contacts available.");
                return;
            }

            Console.WriteLine("\n--- All Contacts ---");
            for (int i = 0; i < contacts.Count; i++)
            {
                // Displaying user-friendly index (i + 1)
                var c = contacts[i];
                Console.WriteLine($"{i + 1}. {c.FirstName} {c.LastName} ({c.MobileNumber})");
            }
        }

        // Retrieves a contact by its zero-based index.
        public Contact? GetContact(int index)
        {
            if (index < 0 || index >= contacts.Count)
            {
                return null;
            }
            return contacts[index];
        }

        /// Replaces an existing contact with an updated contact object.
        public void UpdateContact(int index, Contact updatedContact)
        {
            if (index < 0 || index >= contacts.Count)
            {
                Console.WriteLine("❌ Invalid contact number for update.");
                return;
            }
            // retrieved by GetContact are often enough, but replacing it explicitly is clean.
            contacts[index] = updatedContact;
            Console.WriteLine("✅ Contact updated successfully!");
        }

        /// Deletes a contact at the specified zero-based index.
        public void DeleteContact(int index)
        {
            if (index < 0 || index >= contacts.Count)
            {
                Console.WriteLine("❌ Invalid contact number for deletion.");
                return;
            }
            
            string deletedName = $"{contacts[index].FirstName} {contacts[index].LastName}";
            contacts.RemoveAt(index);
            Console.WriteLine($"✅ Contact '{deletedName}' deleted successfully!");
        }

        /// <summary>
        public int Count => contacts.Count;
    }
}