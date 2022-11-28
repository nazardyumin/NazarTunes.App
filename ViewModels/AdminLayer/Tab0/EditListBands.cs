using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListBands : EditAbstract
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
                    TextField1 = RefDatabase.Bands![_selectedIndex].BandName;
                }
            }
        }

        public EditListBands(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
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
            _refDb.UpdateBand(RefDatabase.Bands![index].BandId, TextField1!);
            RefDatabase.RefreshView();
            var thisBand = RefDatabase.Bands!.Find(b => b.BandName == TextField1!);
            SelectedIndex = RefDatabase.Bands.IndexOf(thisBand!);
        }
    }
}
