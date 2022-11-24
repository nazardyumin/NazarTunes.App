﻿using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListBands : EditAbstract
    {  
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = refDatabase.Bands![_selectedIndex].BandName;
                }
            }
        }

        public EditListBands(ref AdminLayerDb db, ref Database database) : base(ref db, ref database)
        {
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
            _refDb.UpdateBand(refDatabase.Bands![index].BandId, TextField1!);
            refDatabase.RefreshView();
            var thisBand = refDatabase.Bands!.Find(b => b.BandName == TextField1!);
            SelectedIndex = refDatabase.Bands.IndexOf(thisBand!);
        }
    }
}
