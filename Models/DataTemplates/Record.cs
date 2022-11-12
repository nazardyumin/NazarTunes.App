using System.Collections.Generic;

namespace NazarTunes.Models.DataTemplates
{
    public class Record
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int TrackAmount { get; set; }
        public List<string>? Tracks { get; set; }
        public List<string>? Genres { get; set; }
        public List<string>? Performers { get; set; }
        public string? TotalDuration { get; set; }
        public string? Publisher { get; set; }
        public string? ReleaseYear { get; set; }
        public string? MediaFormat { get; set; }
        public string? CoverPath { get; set; }
    }
}
