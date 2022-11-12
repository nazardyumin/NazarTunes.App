namespace NazarTunes.Models.DataTemplates
{
    public abstract class AbstractUser
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }
}
