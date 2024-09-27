namespace _6_CustomAuthenticationAndAuthorization.Domains
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Use proper password hashing in production!
        public string Role { get; set; }
    }
}
