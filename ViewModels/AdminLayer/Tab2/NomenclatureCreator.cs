using System.Collections.Generic;

namespace NazarTunes.ViewModels.AdminLayer.Tab2
{
    public class NomenclatureCreator : NomenclatureConstructorAbstract
    {
        public List<string> GetBands()
        {
            if (Bands is null) return new List<string>();
            return MakeList(Bands!);
        }

        public List<string> GetTracks()
        {
            return MakeList(Tracks!);
        }

        public List<(string, string)> GetPerformers()
        {
            if (Performers is null) return new List<(string, string)>();
            return SplitToFirstAndLastNames(MakeList(Performers!));
        }

        public List<string> GetGenres()
        {
            return MakeList(Genres!);
        }

        public (string newTitle, string newDuration, string newPublisher, string newYear, string newFormat, string newCover, double newPrice) GetFieldsToCreate()
        {
            CoverPath ??= string.Empty;
            return (Title!, FormatDuration(TotalDuration!), Publisher!, ReleaseYear!, MediaFormat!, CoverPath!, GetPrice());
        }

        public void Clear()
        {
            Title = Bands = Performers = Genres = Tracks = TotalDuration = Publisher =
            ReleaseYear = MediaFormat = CoverPath = SellPrice1 = SellPrice2 = string.Empty;
        }

        public void AddBand(string band)
        {
            Bands += $"{band}\r\n";
        }

        public void AddPerformer(string performer)
        {
            Performers += $"{performer}\r\n";
        }

        public void AddGenre(string genre)
        {
            Genres += $"{genre}\r\n";
        }
    }
}
