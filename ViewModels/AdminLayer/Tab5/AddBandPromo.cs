using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class AddBandPromo : AddAbstractPromo
    {
        private Band? _selectedBand;
        public Band? SelectedBand
        {
            get => _selectedBand;
            set => SetField(ref _selectedBand, value);
        }

        public AddBandPromo(ref AdminLayerDb db, ref Database database) : base(ref db, ref database) { }

        public override void Hide()
        {
            Clear();
            IsVisible = Visibility.Collapsed;
            SelectedBand = null;
        }

        protected override void AddFunction()
        {
            _refDb.AddBandPromo(SelectedBand!.BandId, int.Parse(Discount!), StartPromo);
            Clear();
            SelectedBand = null;
            _refDatabase.RefreshPromotions();
            _refDatabase.RefreshNomenclaturesOnly();
        }
    }
}
