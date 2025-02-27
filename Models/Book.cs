using System.ComponentModel.DataAnnotations; // ✅ This namespace provides attributes for data validation.

namespace LibraryManagementSystem.Models
{
    // ✅ This class represents the structure of a Book in the system.
    public class Book
    {
        [Key] // 🔹 Marks this property as the Primary Key (unique identifier for each book).
        public int Id { get; set; } // ✅ Each book will have a unique ID in the database.

        [Required] // 🔹 Ensures that this field cannot be empty (Title is mandatory).
        public string Title { get; set; } // ✅ Stores the book title (e.g., "C# Programming Basics").

        [Required] // 🔹 Ensures that this field cannot be empty (Author is mandatory).
        public string Author { get; set; } // ✅ Stores the name of the book's author.

        [Required] // 🔹 Ensures that this field cannot be empty (ISBN is mandatory).
        public string ISBN { get; set; } // ✅ ISBN (International Standard Book Number) - unique book identifier.

        public int CopiesAvailable { get; set; } // ✅ Number of copies of the book available in the library.

        public int TimesBorrowed { get; set; } // ✅ Tracks how many times this book has been borrowed (useful for ranking popular books).
    }
}
