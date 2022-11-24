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
                if (_selectedIndex > -1)
                {
                    TextField1 = refDatabase.Genres![_selectedIndex].GenreName;
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
            _refDb.UpdateGenre(refDatabase.Genres![index].GenreId, TextField1!);
            refDatabase.RefreshView();
            var thisGenre = refDatabase.Genres!.Find(g => g.GenreName == TextField1!);
            SelectedIndex = refDatabase.Genres!.IndexOf(thisGenre!); 
        }
    }
}
