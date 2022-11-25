﻿using NazarTunes.Models.DataTemplates;
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
            set => SetField(ref _selectedId, value);
        }

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
            SellPrice = nomenclature.SellPrice.ToString();
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

        public void Clear()
        {
            SelectedId = Title = Bands = Performers = Genres =
                Tracks = TotalDuration = Publisher = ReleaseYear =
                MediaFormat = CoverPath = SellPrice = HelperText = string.Empty;
        }
    }
}
