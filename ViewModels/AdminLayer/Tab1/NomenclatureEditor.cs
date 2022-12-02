using NazarTunes.Models.DataTemplates;
using System.Collections.Generic;
using System.Text;

namespace NazarTunes.ViewModels.AdminLayer.Tab1
{
    public class NomenclatureEditor : NomenclatureConstructorAbstract
    {
        private string? _selectedId;
        public string? SelectedId
        {
            get => _selectedId;
            set
            {
                RemoveLettersOrSymbols(ref value!, "digits", 0);
                SetField(ref _selectedId, value);
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
            CoverPath = nomenclature.Record!.CoverPath;
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

        private void SplitSellPrice(ref Nomenclature nomenclature)
        {
            if (nomenclature.SellPrice != 0)
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

        private new void RefreshCanClearState()
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
