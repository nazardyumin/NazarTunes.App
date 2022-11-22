using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab0
{
    public class EditListPerformers : EditAbstract
    {
        private List<Performer>? _performers;
        public List<Performer>? Performers
        {
            get => _performers;
            set => SetField(ref _performers, value);
        }

        private string? _textField2;
        public string? TextField2
        {
            get => _textField2;
            set
            {
                SetField(ref _textField2, value);
                RefreshCanSaveChangesState();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetField(ref _selectedIndex, value);
                if (_selectedIndex > -1)
                {
                    TextField1 = Performers![_selectedIndex].FirstName;
                    TextField2 = Performers![_selectedIndex].LastName;
                }
            }
        }

        public EditListPerformers(ref AdminLayerDb db, Action refreshDb) : base(ref db, refreshDb)
        {
            Performers = new List<Performer>(_refDb.GetAllPerformers());
            SelectedIndex = -1;
        }

        public override void Hide()
        {
            TextField1 = string.Empty;
            TextField2 = string.Empty;
            IsVisible = Visibility.Collapsed;           
            SelectedIndex = -1;
        }

        protected new void RefreshCanSaveChangesState()
        {
            if (string.IsNullOrWhiteSpace(TextField1) && string.IsNullOrWhiteSpace(TextField2) || _selectedIndex == -1)
            {
                CanSaveChanges = false;
            }
            else
            {
                CanSaveChanges = true;
            }
        }

        protected override void SaveChangesFunction()
        {
            var index = SelectedIndex;
            _refDb.UpdatePerformer(Performers![index].PersonId,TextField1!, TextField2!);
            _refreshDb.Invoke();
            Performers = new List<Performer>(_refDb.GetAllPerformers());
            SelectedIndex = index;
        }
    }
}
