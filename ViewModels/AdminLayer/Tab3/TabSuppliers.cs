using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab3
{
    public class TabSuppliers : Notifier
    {
        private EditListSuppliers? _editSuppliers;
        public EditListSuppliers? EditSuppliers
        {
            get => _editSuppliers;
            set => SetField(ref _editSuppliers, value);
        }

        private AddSupplier? _addSupplier;
        public AddSupplier? AddSupplier
        {
            get => _addSupplier;
            set => SetField(ref _addSupplier, value);
        }

        public MyCommand CommandOpenCloseEditSuppliers { get; }
        public MyCommand CommandOpenCloseAddSupplier { get; }

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        public TabSuppliers(ref AdminLayerDb db, ref Database database)
        {
            EditSuppliers = new(ref db, ref database);
            AddSupplier = new(ref db, ref database, EditSuppliers!.GetAction());

            CommandOpenCloseEditSuppliers = new(_ =>
            {
                OpenCloseEditSuppliersFunction();
            }, _ => true);

            CommandOpenCloseAddSupplier = new(_ =>
            {
                OpenCloseAddSupplierFunction();
            }, _ => true);

            ButtonPanelHeight = 47;
        }

        private void OpenCloseEditSuppliersFunction()
        {
            AddSupplier!.Hide();
            ButtonPanelHeight = EditSuppliers!.OpenClose();
        }

        private void OpenCloseAddSupplierFunction()
        {
            EditSuppliers!.Hide();
            ButtonPanelHeight = AddSupplier!.OpenClose();
        }
    }
}
