using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class AddRecordPromo : AddAbstractPromo
    {
        private Nomenclature? _selectedNomenclature;
        public Nomenclature? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        public AddRecordPromo(ref AdminLayerDb db, ref Database database) : base(ref db, ref database) { }

        public override void Hide()
        {
            Clear();
            IsVisible = Visibility.Collapsed;
            SelectedNomenclature = null;
        }

        protected override void AddFunction()
        {
            //_refDb.AddRecordPromo(SelectedNomenclature!.NomenclatureId, int.Parse(Discount!), StartPromo);
            Clear();
            SelectedNomenclature = null;
            _refDatabase.RefreshPromotions();
            _refDatabase.RefreshNomenclaturesOnly();
        }
    }
}
