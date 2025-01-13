namespace API.Repositories
{
    public class Hashing
    {
        public static string GetSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string GenerateHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetSalt());
        }
        public static bool ValidatePassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
