namespace NazarTunes.Models.DataTemplates
{
    public class Performer
    {
        public int PersonId { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
