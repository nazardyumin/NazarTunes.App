using System;
using System.Collections.Generic;

namespace NazarTunes.App.Models.DataTemplates
{
    public class Record
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int TrackAmount { get; set; }
        public DateOnly TotalDuration { get; set; }
        public string? Publisher { get; set; }
        public string? ReleaseYear { get; set; }
        public string? MediaFormat { get; set; }
        public string? CoverPath { get; set; }
        public List<string>? Genres { get; set; }
        public List<string>? Performers { get; set; }
    }
}
