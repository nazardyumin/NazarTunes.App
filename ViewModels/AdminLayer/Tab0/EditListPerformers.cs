using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListPerformers : EditAbstract
    {
        private string? _textField2;
        public string? TextField2
        {
            get => _textField2;
            set
            {
                SetField(ref _textField2, value);
                CommandSaveChanges!.OnCanExecuteChanged();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                CommandSaveChanges!.OnCanExecuteChanged();
                if (_selectedIndex > -1)
                {
                    TextField1 = _refDatabase.Performers![_selectedIndex].FirstName;
                    TextField2 = _refDatabase.Performers![_selectedIndex].LastName;
                }
            }
        }

        public EditListPerformers(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
        {
            SelectedIndex = -1;
        }

        public override void Hide()
        {
            TextField1 = string.Empty;
            TextField2 = string.Empty;
            IsVisible = Visibility.Collapsed;
            SelectedIndex = -1;
        }

        protected override bool RefreshCanSaveChangesState()
        {
            if ((string.IsNullOrWhiteSpace(TextField1) && string.IsNullOrWhiteSpace(TextField2)) || _selectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void SaveChangesFunction()
        {
            var index = SelectedIndex;
            _refDb.UpdatePerformer(_refDatabase.Performers![index].PerformerId, TextField1!, TextField2!);
            _refDatabase.RefreshNomenclaturesAndLists();
            var thisPerformer = _refDatabase.Performers!.Find(p => p.FirstName == TextField1! && p.LastName == TextField2!);
            SelectedIndex = _refDatabase.Performers!.IndexOf(thisPerformer!);
        }
    }
}
