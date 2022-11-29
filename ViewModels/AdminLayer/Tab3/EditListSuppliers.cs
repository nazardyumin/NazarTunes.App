using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.AdminLayer.Tab0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab3
{
    public class EditListSuppliers : EditAbstract
    {
        private List<Supplier>? _suppliers;
        public List<Supplier>? Suppliers
        {
            get => _suppliers;
            set => SetField(ref _suppliers, value);
        }

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
                    TextField1 = Suppliers![_selectedIndex].SupplierName;
                    TextField2 = Suppliers![_selectedIndex].ContactInfo;
                    Flag = Suppliers![_selectedIndex].IsCooperating;
                }
            }
        }

        public EditListSuppliers(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
        {
            SelectedIndex = -1;
            Suppliers = new List<Supplier>(_refDatabase.Suppliers!.OrderBy(b => b.SupplierName).ToList());
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
            _refDb.UpdateSupplier(Suppliers![index].SupplierId, TextField1!, TextField2!, Flag);
            _refDatabase.RefreshSuppliers();
            RefreshSortedSuppliers();
            var thisSupplier = Suppliers!.Find(s => s.SupplierName == TextField1! && s.ContactInfo == TextField2!);
            SelectedIndex = Suppliers!.IndexOf(thisSupplier!);
        }

        private void RefreshSortedSuppliers()
        {
            Suppliers = new List<Supplier>(_refDatabase.Suppliers!.OrderBy(b => b.SupplierName).ToList());
        }

        public Action GetAction()
        {
            return RefreshSortedSuppliers;
        }
    }
}
