using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListBands : EditAbstract
    {
        private List<Band>? _band;
        public List<Band>? Bands
        {
            get => _band;
            set => SetField(ref _band, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = Bands![_selectedIndex].BandName;
                }
            }
        }

        public EditListBands(ref AdminLayerDb db, Action refreshDb) : base(ref db, refreshDb)
        {
            Bands = new List<Band>(_refDb.GetAllBands());
            SelectedIndex = -1;
        }

        public override void Hide()
        {
            TextField1 = string.Empty;
            IsVisible = Visibility.Collapsed;
            SelectedIndex = -1;
        }

        protected override void SaveChangesFunction()
        {
            var index = SelectedIndex;
            _refDb.UpdateBand(Bands![index].BandId, TextField1!);
            _refreshDb.Invoke();
            Bands = new List<Band>(_refDb.GetAllBands());
            SelectedIndex = index;
        }
    }
}
