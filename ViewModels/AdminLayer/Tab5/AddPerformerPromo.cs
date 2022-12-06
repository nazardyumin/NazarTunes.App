using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class AddPerformerPromo : AddAbstractPromo
    {
        private Performer? _selectedPerformer;
        public Performer? SelectedPerformer
        {
            get => _selectedPerformer;
            set => SetField(ref _selectedPerformer, value);
        }

        public AddPerformerPromo(ref AdminLayerDb db, ref Database database) : base(ref db, ref database) { }

        public override void Hide()
        {
            Clear();
            IsVisible = Visibility.Collapsed;
            SelectedPerformer = null;
        }

        protected override void AddFunction()
        {
            //_refDb.AddPerformerPromo(SelectedPerformer!.PerformerId, int.Parse(Discount!), StartPromo);
            Clear();
            SelectedPerformer = null;
            _refDatabase.RefreshPromotions();
            _refDatabase.RefreshNomenclaturesOnly();
        }
    }
}
