using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class AddGenrePromo : AddAbstractPromo
    {
        private Genre? _selectedGenre;
        public Genre? SelectedGenre 
        {
            get => _selectedGenre;
            set => SetField(ref _selectedGenre, value);
        }

        public AddGenrePromo(ref AdminLayerDb db, ref Database database) : base(ref db, ref database) { }

        public override void Hide()
        {
            Clear();
            IsVisible = Visibility.Collapsed;
            SelectedGenre = null;
        }

        protected override void AddFunction()
        {
            _refDb.AddGenrePromo(SelectedGenre!.GenreId, int.Parse(Discount!), StartPromo);
            Clear();
            SelectedGenre = null;
            _refDatabase.RefreshPromotions();
            _refDatabase.RefreshNomenclaturesOnly(); 
        }
    }
}
