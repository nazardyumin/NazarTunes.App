using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab3
{
    public class EditListSuppliers : EditAbstract
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

        private bool _flag;
        public bool Flag
        {
            get => _flag;
            set => SetField(ref _flag, value);
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
                    TextField1 = _refDatabase.SortedSuppliers![_selectedIndex].SupplierName;
                    TextField2 = _refDatabase.SortedSuppliers![_selectedIndex].ContactInfo;
                    Flag = _refDatabase.SortedSuppliers![_selectedIndex].IsCooperating;
                }
            }
        }

        public EditListSuppliers(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
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
            if (string.IsNullOrWhiteSpace(TextField1) || string.IsNullOrWhiteSpace(TextField2) || _selectedIndex == -1)
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
            _refDb.UpdateSupplier(_refDatabase.SortedSuppliers![index].SupplierId, TextField1!, TextField2!, Flag);
            _refDatabase.RefreshSuppliers();
            var thisSupplier = _refDatabase.SortedSuppliers!.Find(s => s.SupplierName == TextField1! && s.ContactInfo == TextField2!);
            SelectedIndex = _refDatabase.SortedSuppliers!.IndexOf(thisSupplier!);
        }
    }
}
