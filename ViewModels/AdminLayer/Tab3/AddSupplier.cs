using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using System;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab3
{
    public class AddSupplier : EditAbstract
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

        private readonly Action _refreshSortedSuppliers;

        public AddSupplier(ref AdminLayerDb db, ref Database database, Action action) : base(ref db, ref database)
        {
            _refreshSortedSuppliers = action;
        }

        public override void Hide()
        {
            TextField1 = string.Empty;
            TextField2 = string.Empty;
            IsVisible = Visibility.Collapsed;
        }

        protected override bool RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) || string.IsNullOrWhiteSpace(TextField2))
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
            _refDb.AddSupplier(TextField1!, TextField2!);
            _refDatabase.RefreshSuppliers();
            _refreshSortedSuppliers.Invoke();
        }
    }
}
