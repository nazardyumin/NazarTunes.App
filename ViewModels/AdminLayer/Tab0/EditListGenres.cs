using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListGenres : EditAbstract
    {
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                CommandSaveChanges!.OnCanExecuteChanged();
                if (_selectedIndex > -1)
                {
                    TextField1 = _refDatabase.Genres![_selectedIndex].GenreName;
                }
            }
        }

        public EditListGenres(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
        {
            SelectedIndex = -1;
        }

        public override void Hide()
        {
            TextField1 = string.Empty;
            IsVisible = Visibility.Collapsed;
            SelectedIndex = -1;
        }

        protected override void SaveChangesFunction()
        {
            var index = SelectedIndex;
            _refDb.UpdateGenre(_refDatabase.Genres![index].GenreId, TextField1!);
            _refDatabase.RefreshNomenclaturesAndLists();
            var thisGenre = _refDatabase.Genres!.Find(g => g.GenreName == TextField1!);
            SelectedIndex = _refDatabase.Genres!.IndexOf(thisGenre!);
        }
    }
}
