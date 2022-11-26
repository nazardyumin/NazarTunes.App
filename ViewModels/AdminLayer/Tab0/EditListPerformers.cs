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
                RefreshCanSaveChangesState();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = RefDatabase.Performers![_selectedIndex].FirstName;
                    TextField2 = RefDatabase.Performers![_selectedIndex].LastName;
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

        protected new void RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) && string.IsNullOrWhiteSpace(TextField2) || _selectedIndex == -1)
            {
                CanSaveChanges = false;
            }
            else
            {
                CanSaveChanges = true;
            }
        }

        protected override void SaveChangesFunction()
        {
            var index = SelectedIndex;
            _refDb.UpdatePerformer(RefDatabase.Performers![index].PersonId, TextField1!, TextField2!);
            RefDatabase.RefreshView();
            var thisPerformer = RefDatabase.Performers!.Find(p => p.FirstName == TextField1! && p.LastName == TextField2!);
            SelectedIndex = RefDatabase.Performers!.IndexOf(thisPerformer!);
        }
    }
}
