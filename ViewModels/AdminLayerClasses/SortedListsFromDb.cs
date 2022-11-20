using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayerClasses
{
    public class SortedListsFromDb : Notifier
    {
        private ObservableCollection<Band>? _band;
        public ObservableCollection<Band>? Bands
        {
            get => _band;
            set => SetField(ref _band, value);
        }

        private ObservableCollection<Performer>? _performers;
        public ObservableCollection<Performer>? Performers
        {
            get => _performers;
            set => SetField(ref _performers, value);
        }

        private ObservableCollection<Genre>? _genre;
        public ObservableCollection<Genre>? Genres
        {
            get => _genre;
            set => SetField(ref _genre, value);
        }

        public SortedListsFromDb(ref AdminLayerDb db)
        {
            Bands = new ObservableCollection<Band>(db.GetAllBands());
            Performers = new ObservableCollection<Performer>(db.GetAllPerformers());
            Genres = new ObservableCollection<Genre>(db.GetAllGenres());
        }


    }
}
