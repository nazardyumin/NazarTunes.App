namespace NazarTunes.Models.DataTemplates
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }

        public override string ToString()
        {
            return GenreName!;
        }
    }
}
