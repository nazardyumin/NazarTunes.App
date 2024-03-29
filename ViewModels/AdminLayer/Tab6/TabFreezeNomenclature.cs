﻿using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;

namespace NazarTunes.ViewModels.AdminLayer.Tab6
{
    public class TabFreezeNomenclature : Notifier
    {
        public FreezeNomenclature FreezeNomenclature { get;set;}

        private int _buttonPanelHeight;
        public int ButtonPanelHeight
        {
            get => _buttonPanelHeight;
            set => SetField(ref _buttonPanelHeight, value);
        }

        private FrozenNomenclature? _selectedFrozenNomenclature;
        public FrozenNomenclature? SelectedFrozenNomenclature
        {
            get => _selectedFrozenNomenclature;
            set => SetField(ref _selectedFrozenNomenclature, value);
        }

        public MyCommand CommandOpenCloseFreezeNomenclature { get; }
        public MyCommand CommandUnfreezeNomenclature { get; }

        public TabFreezeNomenclature(ref AdminLayerDb db, ref Database database)
        {
            FreezeNomenclature = new(ref db, ref database);

            CommandOpenCloseFreezeNomenclature = new(_ =>
            {
                ButtonPanelHeight = FreezeNomenclature.OpenClose();
            }, _ => true);

            CommandUnfreezeNomenclature = new(_ =>
            {
                FreezeNomenclature.UnfreezeNomenclatureFunction(SelectedFrozenNomenclature!.FrozenNomenclatureId);
            }, _ => true);

            ButtonPanelHeight = 47;
        }
    }
}
