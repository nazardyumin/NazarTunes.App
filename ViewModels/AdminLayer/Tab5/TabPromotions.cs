using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab5
{
    public class TabPromotions : Notifier
    {
        public AddGenrePromo AddGenrePromo { get; set; }
        public AddBandPromo AddBandPromo { get; set; }
        public AddPerformerPromo AddPerformerPromo { get; set; }
        public AddRecordPromo AddRecordPromo { get; set; }

        public StartOrFinishPromo StartPromo { get; set; }
        public StartOrFinishPromo FinishPromo { get; set; }

        public MyCommand CommandOpenCloseAddGenrePromo { get; }
        public MyCommand CommandOpenCloseAddBandPromo { get; }
        public MyCommand CommandOpenCloseAddPerformerPromo { get; }
        public MyCommand CommandOpenCloseAddRecordPromo { get; }

        public MyCommand CommandOpenCloseStartPromo { get; }
        public MyCommand CommandOpenCloseFinishPromo { get; }

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        public TabPromotions(ref AdminLayerDb db, ref Database database)
        {
            AddGenrePromo = new(ref db, ref database);
            CommandOpenCloseAddGenrePromo = new(_ =>
            {
                ButtonPanelHeight = AddGenrePromo.OpenClose();
                AddBandPromo?.Hide();
                AddPerformerPromo?.Hide();
                AddRecordPromo?.Hide();
                FinishPromo?.Hide();
                StartPromo?.Hide();
            }, _ => true);

            AddBandPromo = new(ref db, ref database);
            CommandOpenCloseAddBandPromo = new(_ =>
            {
                ButtonPanelHeight = AddBandPromo.OpenClose();
                AddGenrePromo?.Hide();
                AddPerformerPromo?.Hide();
                AddRecordPromo?.Hide();
                FinishPromo?.Hide();
                StartPromo?.Hide();
            }, _ => true);

            AddPerformerPromo = new(ref db, ref database);
            CommandOpenCloseAddPerformerPromo = new(_ =>
            {
                ButtonPanelHeight = AddPerformerPromo.OpenClose();
                AddGenrePromo?.Hide();
                AddBandPromo?.Hide();
                AddRecordPromo?.Hide();
                FinishPromo?.Hide();
                StartPromo?.Hide();
            }, _ => true);

            AddRecordPromo = new(ref db, ref database);
            CommandOpenCloseAddRecordPromo = new(_ =>
            {
                ButtonPanelHeight = AddRecordPromo.OpenClose();
                AddGenrePromo?.Hide();
                AddBandPromo?.Hide();
                AddPerformerPromo?.Hide();
                FinishPromo?.Hide();
                StartPromo?.Hide();
            }, _ => true);

            StartPromo = new(ref db, ref database, true);
            CommandOpenCloseStartPromo = new(_ =>
            {
                ButtonPanelHeight = StartPromo.OpenClose();
                AddGenrePromo?.Hide();
                AddBandPromo?.Hide();
                AddPerformerPromo?.Hide();
                AddRecordPromo?.Hide();
                FinishPromo?.Hide();
            }, _ => true);

            FinishPromo = new(ref db, ref database, false);
            CommandOpenCloseFinishPromo = new(_ =>
            {
                ButtonPanelHeight = FinishPromo.OpenClose();
                AddGenrePromo?.Hide();
                AddBandPromo?.Hide();
                AddPerformerPromo?.Hide();
                AddRecordPromo?.Hide();
                StartPromo?.Hide();
            }, _ => true);

            ButtonPanelHeight = 47;
        }
    }
}
