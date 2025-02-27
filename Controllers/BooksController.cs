// ✅ Import necessary namespaces
using LibraryManagementSystem.Data; // Import database context to interact with the database.
using LibraryManagementSystem.Models; // Import the Book model.
using Microsoft.AspNetCore.Authorization; // Import authorization for role-based access.
using Microsoft.AspNetCore.Mvc; // Import ASP.NET Core API controller functionalities.
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core for database operations.

namespace LibraryManagementSystem.Controllers
{
    // ✅ Define API route (Base URL: http://localhost:5169/api/books)
    [Route("api/books")]
    [ApiController] // Marks this as an API controller (automatic request validation & response handling).
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context; // ✅ Database context to interact with the Books table.

        // ✅ Constructor: Injects the database context when this controller is created.
        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL BOOKS (PUBLIC ACCESS)
        // 🔹 This allows **anyone** (Admin or regular User) to fetch all books.
        [HttpGet] // GET request → http://localhost:5169/api/books
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _context.Books.ToListAsync()); // ✅ Fetch all books from the database and return them.
        }

        // ✅ GET BOOKS GROUPED BY AUTHOR (PUBLIC ACCESS)
        // 🔹 This groups books by **Author name** and returns them in a structured format.
        [HttpGet("grouped-by-author")] // GET request → http://localhost:5169/api/books/grouped-by-author
        public IActionResult GetBooksGroupedByAuthor()
        {
            var groupedBooks = _context.Books
                .GroupBy(b => b.Author) // ✅ Group books by Author name.
                .Select(g => new { Author = g.Key, Books = g.ToList() }) // ✅ Create a new object with the author's name and book list.
                .ToList();

            return Ok(groupedBooks); // ✅ Return the grouped books as JSON.
        }

        // ✅ GET TOP 3 MOST BORROWED BOOKS (PUBLIC ACCESS)
        // 🔹 This returns the **3 books that have been borrowed the most** (sorted by `TimesBorrowed`).
        [HttpGet("most-borrowed")] // GET request → http://localhost:5169/api/books/most-borrowed
        public IActionResult GetMostBorrowedBooks()
        {
            var topBooks = _context.Books
                .OrderByDescending(b => b.TimesBorrowed) // ✅ Sort books in descending order by `TimesBorrowed`.
                .Take(3) // ✅ Select only the top 3 books.
                .ToList();

            return Ok(topBooks); // ✅ Return the top 3 most borrowed books.
        }

        // ✅ ADD A NEW BOOK (ADMIN ONLY)
        // 🔹 Only **Admins** can add books to the library.
        [Authorize(Roles = "Admin")] // 🔒 Restrict access to Admins only.
        [HttpPost] // POST request → http://localhost:5169/api/books
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            _context.Books.Add(book); // ✅ Add the new book to the database.
            await _context.SaveChangesAsync(); // ✅ Save changes in the database.
            return Ok("Book added successfully."); // ✅ Return success message.
        }

        // ✅ DELETE A BOOK (ADMIN ONLY)
        // 🔹 Only **Admins** can delete books from the library.
        [Authorize(Roles = "Admin")] // 🔒 Restrict access to Admins only.
        [HttpDelete("{id}")] // DELETE request → http://localhost:5169/api/books/{id}
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id); // ✅ Find the book by ID.
            if (book == null) return NotFound(); // ✅ If book is not found, return 404 Not Found.
            
            _context.Books.Remove(book); // ✅ Remove the book from the database.
            await _context.SaveChangesAsync(); // ✅ Save changes in the database.
            return Ok("Book deleted successfully."); // ✅ Return success message.
        }
    }
}
