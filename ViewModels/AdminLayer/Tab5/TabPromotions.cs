using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class TabPromotions : Notifier
    {
        public AddGenrePromo AddGenrePromo { get; set; }   


        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        public MyCommand CommandOpenCloseAddGenrePromo { get; }


        public TabPromotions(ref AdminLayerDb db, ref Database database)
        {
            AddGenrePromo = new(ref db, ref database);
            CommandOpenCloseAddGenrePromo = new(_ =>
            {
                ButtonPanelHeight = AddGenrePromo.OpenClose();
            }, _ => true);


            ButtonPanelHeight = 47;
        }
    }
}
