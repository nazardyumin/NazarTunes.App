using NazarTunes.ViewModels.Notifiers;
using System.Security.Policy;

namespace NazarTunes.ViewModels
{
    public class NomenclatureConstructor : Notifier
    {
        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        private string? _bands;
        public string? Bands
        {
            get => _bands;
            set => SetField(ref _bands, value);
        }

        private string? _performers;
        public string? Performers
        {
            get => _performers;
            set => SetField(ref _performers, value);
        }

        private string? _genres;
        public string? Genres
        {
            get => _genres;
            set => SetField(ref _genres, value);
        }

        private string? _tracks;
        public string? Tracks
        {
            get => _tracks;
            set => SetField(ref _tracks, value);
        }

        private string? _totalDuration;
        public string? TotalDuration
        {
            get => _totalDuration;
            set => SetField(ref _totalDuration, value);
        }

        private string? _publisher;
        public string? Publisher
        {
            get => _publisher;
            set => SetField(ref _publisher, value);
        }

        private string? _releaseYear;
        public string? ReleaseYear
        {
            get => _releaseYear;
            set => SetField(ref _releaseYear, value);
        }

        private string? _mediaFormat;
        public string? MediaFormat
        {
            get => _mediaFormat;
            set => SetField(ref _mediaFormat, value);
        }

        private string? _coverPath;
        public string? CoverPath
        {
            get => _coverPath;
            set => SetField(ref _coverPath, value);
        }

        private string? _sellPrice;
        public string? SellPrice
        {
            get => _sellPrice;
            set => SetField(ref _sellPrice, value);
        }

        private string? _helperText;
        public string? HelperText
        {
            get => _helperText;
            set => SetField(ref _helperText, value);
        }
    }
}
