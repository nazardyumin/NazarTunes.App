using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListGenres : EditAbstract
    {
        private List<Genre>? _genre;
        public List<Genre>? Genres
        {
            get => _genre;
            set => SetField(ref _genre, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = Genres![_selectedIndex].GenreName;
                }
            }
        }

        public EditListGenres(ref AdminLayerDb db, Action refreshDb) : base(ref db, refreshDb)
        {
            Genres = new List<Genre>(_refDb.GetAllGenres());
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
            _refDb.UpdateGenre(Genres![index].GenreId, TextField1!);
            _refreshDb.Invoke();
            Genres = new List<Genre>(_refDb.GetAllGenres());
            SelectedIndex = index;
        }
    }
}
