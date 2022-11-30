using NazarTunes.ViewModels.Notifiers;
using System;
using System.Collections.Generic;

namespace NazarTunes.ViewModels.AdminLayer
{
    public abstract class NomenclatureConstructorAbstract : Notifier
    {
        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                SetField(ref _title, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _bands;
        public string? Bands
        {
            get => _bands;
            set
            {
                SetField(ref _bands, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _performers;
        public string? Performers
        {
            get => _performers;
            set
            {
                SetField(ref _performers, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _genres;
        public string? Genres
        {
            get => _genres;
            set
            {
                SetField(ref _genres, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _tracks;
        public string? Tracks
        {
            get => _tracks;
            set
            {
                SetField(ref _tracks, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _totalDuration;
        public string? TotalDuration
        {
            get => _totalDuration;
            set
            {
                RemoveLettersOrSymbols(ref value!, "duration", 0);
                SetField(ref _totalDuration, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _publisher;
        public string? Publisher
        {
            get => _publisher;
            set
            {
                SetField(ref _publisher, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _releaseYear;
        public string? ReleaseYear
        {
            get => _releaseYear;
            set
            {
                RemoveLettersOrSymbols(ref value!, "digits", 4);
                SetField(ref _releaseYear, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _mediaFormat;
        public string? MediaFormat
        {
            get => _mediaFormat;
            set
            {
                SetField(ref _mediaFormat, value);
                RefreshCanSaveState();
                RefreshCanClearState();
            }
        }

        private string? _coverPath;
        public string? CoverPath
        {
            get => _coverPath;
            set
            {
                SetField(ref _coverPath, value);
                ImagePath = value;
                RefreshCanClearState();
            }
        }

        private string? _imagePath;
        public string? ImagePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_imagePath) || !ValidUrl(_imagePath))
                {
                    return @"\Assets\nocover.jpg";
                }
                else return _imagePath;
            }
            set
            {
                SetField(ref _imagePath, value);
            }
        }

        private string? _sellPrice1;
        public string? SellPrice1
        {
            get => _sellPrice1;
            set
            {
                RemoveLettersOrSymbols(ref value!, "digits", 0);
                SetField(ref _sellPrice1, value);
                RefreshCanClearState();
            }
        }

        private string? _sellPrice2;
        public string? SellPrice2
        {
            get => _sellPrice2;
            set
            {
                RemoveLettersOrSymbols(ref value!, "digits", 2);
                SetField(ref _sellPrice2, value);
                RefreshCanClearState();
            }
        }

        private bool _canSave;
        public bool CanSave
        {
            get => _canSave;
            set => SetField(ref _canSave, value);
        }

        private bool _canClear;
        public bool CanClear
        {
            get => _canClear;
            set => SetField(ref _canClear, value);
        }

        protected List<string> MakeList(string str)
        {
            if (str == string.Empty || str is null)
            {
                return new List<string>();
            }
            else
            {
                if (str[^1] != '\n') str += "\r\n";
                var list = new List<string>();
                while (str != string.Empty)
                {
                    var tmp = str.Substring(0, str.IndexOf('\n') - 1);
                    str = str.Remove(0, str.IndexOf('\n') + 1);
                    if (!string.IsNullOrWhiteSpace(tmp)) list.Add(tmp);
                }
                return list;
            }
        }

        protected List<(string, string)> SplitToFirstAndLastNames(List<string> list)
        {
            var splittedList = new List<(string, string)>();
            foreach (var item in list)
            {
                if (item.Contains(' '))
                {
                    splittedList.Add((item.Substring(0, item.IndexOf(' ')), item.Substring(item.IndexOf(' ') + 1)));
                }
                else
                {
                    splittedList.Add((item, ""));
                }
            }
            return splittedList;
        }

        public double GetPrice()
        {
            if ((string.IsNullOrWhiteSpace(SellPrice1) && string.IsNullOrWhiteSpace(SellPrice2)) || (string.IsNullOrWhiteSpace(SellPrice1) && !string.IsNullOrWhiteSpace(SellPrice2)))
            {
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(SellPrice1) && string.IsNullOrWhiteSpace(SellPrice2))
            {
                return double.Parse(SellPrice1);
            }
            else return double.Parse($"{SellPrice1},{SellPrice2}");
        }

        protected void RemoveLettersOrSymbols(ref string str, string key, int cut)
        {
            if (key == "digits")
            {
                if (!string.IsNullOrEmpty(str))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!char.IsDigit(str[i])) str = str.Remove(i, 1);
                    }
                }
                if (cut == 0) return;
                if (cut == 2)
                {
                    if (str.Length > 2) str = str.Remove(2);

                }
                else 
                {
                    if (str.Length >= 4)
                    {
                        if (str.Length > 4) str = str.Remove(4);
                        var thisYear = DateTime.Now.Year;
                        if (int.Parse(str) > thisYear) str = str.Remove(0);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(str))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!char.IsDigit(str[i]) && str[i] != ':') str = str.Remove(i, 1);
                    }
                }
            }
        }

        protected string FormatDuration(string duration)
        {
            var hours = string.Empty;
            if (duration.Length == 5)
            {
                hours = "00:";
            }
            return $"{hours}{duration}";
        }

        protected void RefreshCanSaveState()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Genres) || string.IsNullOrWhiteSpace(Tracks) ||
                string.IsNullOrWhiteSpace(TotalDuration) || string.IsNullOrWhiteSpace(Publisher) || string.IsNullOrWhiteSpace(ReleaseYear) ||
                string.IsNullOrWhiteSpace(MediaFormat) || (string.IsNullOrWhiteSpace(Bands) && string.IsNullOrWhiteSpace(Performers)))
            {
                CanSave = false;
            }
            else CanSave = true;
        }

        protected void RefreshCanClearState()
        {
            if (string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Bands) && string.IsNullOrWhiteSpace(Performers) &&
                string.IsNullOrWhiteSpace(Genres) && string.IsNullOrWhiteSpace(Tracks) && string.IsNullOrWhiteSpace(TotalDuration) && string.IsNullOrWhiteSpace(Publisher) &&
                string.IsNullOrWhiteSpace(ReleaseYear) && string.IsNullOrWhiteSpace(MediaFormat) && string.IsNullOrWhiteSpace(SellPrice1) && string.IsNullOrWhiteSpace(SellPrice2) &&
                string.IsNullOrWhiteSpace(CoverPath))
            {
                CanClear = false;
            }
            else CanClear = true;
        }

        protected bool ValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult!)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
