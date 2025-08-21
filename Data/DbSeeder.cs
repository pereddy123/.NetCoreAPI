using WorkSphereAPI.Models;

namespace WorkSphereAPI.Data
{

    public static class DbSeeder
    {
        public static void SeedUsers(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"), // This should be hashed!
                    Role = "Admin"
                },
                new User
                {
                    Username = "manager",
                    PasswordHash = HashPassword("manager123"),
                    Role = "Manager"
                },
                new User
                {
                    Username = "employee",
                    PasswordHash = HashPassword("employee123"),
                    Role = "Employee"
                }
            };

                db.Users.AddRange(users);
                db.SaveChanges();
            }

        }


        private static string HashPassword(string password)
        {
            // For now, use a placeholder (you'll replace this with real hashing in auth logic)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

}
