using NazarTunes.Models.DataTemplates;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Text;

namespace NazarTunes.ViewModels.AdminLayer.Tab1
{
    public class NomenclatureConstructor : Notifier
    {
        private string? _selectedId;
        public string? SelectedId
        {
            get => _selectedId;
            set
            {
                RemoveLettersOrSymbols(ref value!, "digits");
                SetField(ref _selectedId, value);
                RefreshCanClearState();
            }          
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                SetField(ref _title, value);
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
                RefreshCanClearState();
            }
        }

        private string? _totalDuration;
        public string? TotalDuration
        {
            get => _totalDuration;
            set
            {
                RemoveLettersOrSymbols(ref value!,"duration");
                SetField(ref _totalDuration, value);
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
                RefreshCanClearState();
            }
        }

        private string? _releaseYear;
        public string? ReleaseYear
        {
            get => _releaseYear;
            set
            {
                RemoveLettersOrSymbols(ref value!,"digits");
                SetField(ref _releaseYear, value);
                RefreshCanSaveChangesState();
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
                RefreshCanSaveChangesState();
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
                RefreshCanClearState();
            }   
        }

        private string? _sellPrice1;
        public string? SellPrice1
        {
            get => _sellPrice1;
            set 
            {
                RemoveLettersOrSymbols(ref value!, "digits");
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
                RemoveLettersOrSymbols(ref value!, "digits");
                SetField(ref _sellPrice2, value);
                RefreshCanClearState();
            }
        }

        private string? _helperText;
        public string? HelperText
        {
            get => _helperText;
            set
            {
                SetField(ref _helperText, value);
                RefreshCanClearState();
            }
        }

        private bool _canSaveChanges;
        public bool CanSaveChanges
        {
            get => _canSaveChanges;
            set => SetField(ref _canSaveChanges, value);
        }

        private bool _canClear;
        public bool CanClear
        {
            get => _canClear;
            set => SetField(ref _canClear, value);
        }

        public bool SelectedIdIsNotNullOrEmpty()
        {
            return !string.IsNullOrWhiteSpace(SelectedId);
        }

        public int GetSelectedId()
        {
            return int.Parse(SelectedId!);
        }

        public int GetSelectedIndex()
        {
            return int.Parse(SelectedId!) - 1;
        }

        public double GetPrice()
        {
            if (string.IsNullOrWhiteSpace(SellPrice1) || string.IsNullOrWhiteSpace(SellPrice2))
            {
                return 0;
            }
            return double.Parse($"{SellPrice1},{SellPrice2}");
        }

        public bool SelectedIdContainsOnlyDigits()
        {
            var result = true;
            foreach (var symbol in SelectedId!)
            {
                if (!char.IsDigit(symbol)) { result = false; break; }
            }
            return result;
        }

        private List<string> MakeList(string str)
        {
            if (str != string.Empty)
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
            else return new List<string>();
        }

        private string MakeColumn(List<string> list)
        {
            var str = new StringBuilder();
            var stop = list.Count - 1;
            foreach (var item in list)
            {
                if (list.IndexOf(item) == stop) { str.Append(item); break; }
                str.Append(item + "\r\n");
            }
            return str.ToString();
        }

        public void Set(Nomenclature nomenclature)
        {
            Title = nomenclature.Record!.Title;
            Bands = MakeColumn(nomenclature.Record!.Bands!);
            Performers = MakeColumn(nomenclature.Record!.Performers!);
            Genres = MakeColumn(nomenclature.Record!.Genres!);
            Tracks = MakeColumn(nomenclature.Record!.Tracks!);
            TotalDuration = nomenclature.Record!.TotalDuration;
            Publisher = nomenclature.Record!.Publisher;
            ReleaseYear = nomenclature.Record!.ReleaseYear;
            MediaFormat = nomenclature.Record!.MediaFormat;
            if (nomenclature.Record!.CoverPath == string.Empty)
            {
                CoverPath = @"\Assets\nocover.jpg";
            }
            else
            {
                CoverPath = nomenclature.Record!.CoverPath;
            }
            SplitSellPrice(ref nomenclature);
            HelperText = string.Empty;
        }

        public (List<string> newBands, List<string> oldBands, string actionKeyBands) CompareNewAndOldBands(Nomenclature nomenclature)
        {
            var newBands = MakeList(Bands!);
            var oldBands = new List<string>(nomenclature.Record!.Bands!);         
            return (newBands, oldBands, CountComparer(newBands, oldBands));
        }

        public (List<string> newTracks, List<string> oldTracks, string actionKeyTracks) CompareNewAndOldTracks(Nomenclature nomenclature)
        {
            var newTracks = MakeList(Tracks!);
            var oldTracks = new List<string>(nomenclature.Record!.Tracks!);
            return (newTracks, oldTracks, CountComparer(newTracks, oldTracks));
        }

        public (List<(string, string)> newPerformers, List<(string, string)> oldPerformermers, string actionKeyPerformers) CompareNewAndOldPerformers(Nomenclature nomenclature)
        {
            var newPerformers = SplitToFirstAndLastNames(MakeList(Performers!));
            var oldPerformers = SplitToFirstAndLastNames(new List<string>(nomenclature.Record!.Performers!));
            return (newPerformers, oldPerformers, CountComparer(newPerformers, oldPerformers));
        }

        public (List<string> newGenres, List<string> oldGenres, string actionKeyGenres) CompareNewAndOldGenres(Nomenclature nomenclature)
        {
            var newGenres = MakeList(Genres!);
            var oldGenres = new List<string>(nomenclature.Record!.Genres!);
            return (newGenres, oldGenres, CountComparer(newGenres, oldGenres));
        }

        public (int idRecord, string newTitle, string newDuration, string newPublisher, string newYear, string newFormat, string newCover) GetFieldsToUpdate()
        {
            return (GetSelectedId(), Title!, FormatDuration(TotalDuration!), Publisher!, ReleaseYear!, MediaFormat!, CoverPath!);
        }

        private string FormatDuration(string duration)
        {
            var hours = string.Empty;
            if (duration.Length==5)
            {
                hours = "00:";
            }
            return $"{hours}{duration}";      
        }

        private string CountComparer(List<string> newList, List<string> oldList)
        {
            if (newList.Count == oldList.Count)
            {
                return "update";
            }
            else if (newList.Count > oldList.Count)
            {
                return "add";
            }
            else
            {
                return "delete";
            }
        }

        private string CountComparer(List<(string, string)> newList, List<(string, string)> oldList)
        {
            if (newList.Count == oldList.Count)
            {
                return "update";
            }
            else if (newList.Count > oldList.Count)
            {
                return "add";
            }
            else
            {
                return "delete";
            }
        }

        private List<(string,string)> SplitToFirstAndLastNames(List<string> list)
        {
            var splittedList = new List<(string, string)>();
            foreach (var item in list)
            {
                if (item.Contains(' '))
                {
                    splittedList.Add((item.Substring(0, item.IndexOf(' ')), item.Substring(item.IndexOf(' ')+1)));
                }
                else
                {
                    splittedList.Add((item,""));
                }
            }
            return splittedList;
        }

        public void Clear(string? error = null)
        {
            if (error is not null)
            {
                Title = Bands = Performers = Genres =
                Tracks = TotalDuration = Publisher = ReleaseYear =
                MediaFormat = CoverPath = SellPrice1 = SellPrice2 = string.Empty;
            }
            else
            {
                SelectedId = Title = Bands = Performers = Genres =
                Tracks = TotalDuration = Publisher = ReleaseYear =
                MediaFormat = CoverPath = SellPrice1 = SellPrice2 = HelperText = string.Empty;
            } 
        }

        private void SplitSellPrice( ref Nomenclature nomenclature)
        {            
            if (nomenclature.SellPrice !=0)
            {
                var price = nomenclature.SellPrice.ToString();
                if (!price.Contains(','))
                {
                    SellPrice1 = price;
                    SellPrice2 = "00";     
                }
                else
                {
                    SellPrice1 = price[..price.IndexOf(',')];
                    SellPrice2 = price[(price.IndexOf(',') + 1)..];
                }
            } 
            else
            {
                SellPrice1 = "0";
                SellPrice2 = "00";
            }
        }

        private void RemoveLettersOrSymbols(ref string str, string key)
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
            }
            else
            {
                if (!string.IsNullOrEmpty(str))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (!char.IsDigit(str[i]) && str[i]!=':') str = str.Remove(i, 1);
                    }
                }
            }     
        }

        private void RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Genres) || string.IsNullOrWhiteSpace(Tracks) ||
                string.IsNullOrWhiteSpace(TotalDuration) || string.IsNullOrWhiteSpace(Publisher) || string.IsNullOrWhiteSpace(ReleaseYear) ||
                string.IsNullOrWhiteSpace(MediaFormat) || (string.IsNullOrWhiteSpace(Bands) && string.IsNullOrWhiteSpace(Performers)))
            {
                CanSaveChanges = false;
            }
            else CanSaveChanges = true;
        }

        private void RefreshCanClearState()
        {
            if (string.IsNullOrWhiteSpace(SelectedId) && string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Bands) && string.IsNullOrWhiteSpace(Performers) &&
                string.IsNullOrWhiteSpace(Genres) && string.IsNullOrWhiteSpace(Tracks) && string.IsNullOrWhiteSpace(TotalDuration) && string.IsNullOrWhiteSpace(Publisher) &&
                string.IsNullOrWhiteSpace(ReleaseYear) && string.IsNullOrWhiteSpace(MediaFormat) && string.IsNullOrWhiteSpace(SellPrice1) && string.IsNullOrWhiteSpace(SellPrice2) &&
                string.IsNullOrWhiteSpace(HelperText) && string.IsNullOrWhiteSpace(CoverPath))
            {
                CanClear = false;   
            }
            else CanClear = true;
        }
    }
}
