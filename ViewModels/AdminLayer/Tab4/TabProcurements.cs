using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab4
{
    public class TabProcurements : Notifier
    {
        private readonly AdminLayerDb _refDb;
        private readonly Database _refDatabase;
        public AddProcurement? AddProcurement { get; set; }

        public MyCommand CommandOpenCloseAddProcurement { get; }
        public MyCommand CommandSave { get; }

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        public TabProcurements(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            AddProcurement = new ();

            CommandOpenCloseAddProcurement = new(_ =>
            {
                ButtonPanelHeight = AddProcurement.OpenClose();
            }, _ => true);

            CommandSave = new(_ =>
            {
                SaveFunction();
            }, _ => AddProcurement.RefreshFieldsState());

            AddProcurement.RefreshStates = CommandSave.OnCanExecuteChanged;

            ButtonPanelHeight = 47;
        }

        private void SaveFunction()
        {
            var (recordIndex, supplierIndex, date, amount, price) = AddProcurement!.GetFields();
            var recordId = _refDatabase!.Nomenclatures![recordIndex].Record!.Id;
            var supplierId = _refDatabase!.SortedSuppliers![supplierIndex].SupplierId;
            _refDb.AddProcurement(recordId, supplierId, date, amount, price);
            AddProcurement.Clear();
            _refDatabase.RefreshProcurements();
            _refDatabase.RefreshNomenclaturesOnly();
        }
    }
}
