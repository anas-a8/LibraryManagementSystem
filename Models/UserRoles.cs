namespace LibraryManagementSystem.Models
{
    // ✅ This enum defines different user roles in the system.
    public enum UserRoles
    {
        Admin, // 🔹 Admin users have full access (can add, edit, delete books).
        User   // 🔹 Regular users can only view book details (read-only access).
    }
}
