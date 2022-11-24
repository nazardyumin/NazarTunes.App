﻿using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;

namespace NazarTunes.ViewModels.AdminLayer.Tab1
{
    public class TabEditNomenclature : Notifier
    {
        private readonly AdminLayerDb _refDb;
        private readonly Database _refDatabase;

        private NomenclatureConstructor? _selectedNomenclature;
        public NomenclatureConstructor? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        public MyCommand CommandFindNomenclature { get; }
        public MyCommand CommandSaveChanges { get; }
        public MyCommand CommandClear { get; }

        public TabEditNomenclature(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            _refDatabase = database;

            SelectedNomenclature = new();

            CommandFindNomenclature = new(_ =>
            {
                FindNomenclatureFunction();
            }, _ => true);
            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => true);
            CommandClear = new(_ =>
            {
                ClearFunction();
            }, _ => true);
        }

        private void FindNomenclatureFunction()
        {
            if (SelectedNomenclature!.SelectedIdIsNotNullOrEmpty())
            {
                if (SelectedNomenclature.SelectedIdContainsOnlyDigits())
                {
                    var i = SelectedNomenclature.GetSelectedIndex();
                    if (i <= _refDatabase!.Nomenclatures!.Count - 1 && i >= 0)
                    {
                        SelectedNomenclature.Set(_refDatabase!.Nomenclatures[i]);
                    }
                    else SelectedNomenclature.HelperText = "Invalid ID!";
                }
                else SelectedNomenclature.HelperText = "This field may contain only digits!";
            }
            else SelectedNomenclature.HelperText = "Enter ID!";
        }

        private void SaveChangesFunction()
        {
            var i = SelectedNomenclature!.GetSelectedIndex();
            

            var (newTracks, oldTracks, actionKeyTracks) = SelectedNomenclature.CompareNewAndOldTracks(_refDatabase!.Nomenclatures![i]);
            UpdateTracks(newTracks, oldTracks, actionKeyTracks);
            
            var (newBands, oldBands, actionKeyBands) = SelectedNomenclature.CompareNewAndOldBands(_refDatabase!.Nomenclatures![i]);
            UpdateBandItems(newBands, oldBands, actionKeyBands);
            //TODO finish this function with all list fields!!!
            //TODO make editions for lists genres, performers!




            _refDatabase.RefreshView();
        }

        private void ClearFunction()
        {
            SelectedNomenclature!.Clear();
        }

        private void UpdateTracks(List<string> newTracks, List<string> oldTracks, string actionKey)
        {
            var idRecord = SelectedNomenclature!.GetSelectedId();
            var trackIds = _refDb.GetAllTrackIds(idRecord);

            if (actionKey == "update")
            {
                int i = 0;
                foreach (var id in trackIds)
                {
                    _refDb.UpdateOneTrack(id, newTracks[i]);
                    i++;
                }
            }
            else if (actionKey == "delete")
            {
                int i = 0;
                foreach (var track in newTracks)
                {
                    _refDb.UpdateOneTrack(trackIds[i], track);
                    i++;
                }
                for (; i < oldTracks.Count; i++)
                {
                    _refDb.DeleteOneTrack(trackIds[i]);
                }
            }
            else
            {
                int i = 0;
                foreach (var track in oldTracks)
                {
                    _refDb.UpdateOneTrack(trackIds[i], track);
                    i++;
                }
                for (; i < newTracks.Count; i++)
                {
                    _refDb.AddOneTrack(idRecord, newTracks[i]);
                }
            } 
        }

        private void UpdateBandItems(List<string> newBands, List<string> oldBands, string actionKey)
        {
            var idRecord = SelectedNomenclature!.GetSelectedId();
            var bandItemIds = _refDb.GetAllBandItemIds(idRecord);

            if (actionKey == "update")
            {
                int i = 0;
                foreach (var id in bandItemIds)
                {
                    _refDb.UpdateBandItem(id, newBands[i]);
                    i++;
                }
            }
            else if (actionKey == "delete")
            {
                int i = 0;
                foreach (var band in newBands)
                {
                    _refDb.UpdateBandItem(bandItemIds[i], band);
                    i++;
                }
                for (; i < oldBands.Count; i++)
                {
                    _refDb.DeleteBandItem(bandItemIds[i]);
                }
            }
            else
            {
                int i = 0;
                foreach (var band in oldBands)
                {
                    _refDb.UpdateBandItem(bandItemIds[i], band);
                    i++;
                }
                for (; i < newBands.Count; i++)
                {
                    _refDb.AddBandItem(idRecord, newBands[i]);
                }
            }
        }
    }
}
