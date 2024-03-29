﻿using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.LanguagePacks;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;

namespace NazarTunes.ViewModels.AdminLayer.Tab1
{
    public class TabEditNomenclature : Notifier
    {
        private readonly AdminLayerDb _refDb;
        private readonly Database _refDatabase;
        private LanguagePack _language;

        private NomenclatureEditor? _selectedNomenclature;
        public NomenclatureEditor? SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }

        public MyCommand CommandFindNomenclature { get; }
        public MyCommand CommandSaveChanges { get; }
        public MyCommand CommandClear { get; }

        private bool _canChangeId;
        public bool CanChangeId
        {
            get => _canChangeId;
            set => SetField(ref _canChangeId, value);
        }

        public TabEditNomenclature(ref AdminLayerDb db, ref Database database, ref LanguagePack language)
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
            CanChangeId = true;

            _language = language;
        }

        private void FindNomenclatureFunction()
        {
            if (SelectedNomenclature!.SelectedIdIsNotNullOrEmpty())
            {
                var i = SelectedNomenclature.GetSelectedIndex();
                if (i <= _refDatabase!.Nomenclatures!.Count - 1 && i >= 0)
                {
                    SelectedNomenclature.Set(_refDatabase!.Nomenclatures[i]);
                    CanChangeId = false;
                }
                else { SelectedNomenclature.HelperText = _language.AdminTabEditAndNewNomenclature.HelperTextInvalidId; ClearFunction("error"); }
            }
            else { SelectedNomenclature.HelperText = _language.AdminTabEditAndNewNomenclature.HelperTextEnterId; ClearFunction("error"); }
        }

        private void SaveChangesFunction()
        {
            var i = SelectedNomenclature!.GetSelectedIndex();
            var (newBands, oldBands, actionKeyBands) = SelectedNomenclature.CompareNewAndOldBands(_refDatabase!.Nomenclatures![i]);
            UpdateBandItems(newBands, oldBands, actionKeyBands);
            var (newPerformers, oldPerformermers, actionKeyPerformers) = SelectedNomenclature.CompareNewAndOldPerformers(_refDatabase!.Nomenclatures![i]);
            UpdatePerformerItems(newPerformers, oldPerformermers, actionKeyPerformers);
            var (newGenres, oldGenres, actionKeyGenres) = SelectedNomenclature.CompareNewAndOldGenres(_refDatabase!.Nomenclatures![i]);
            UpdateGenreItems(newGenres, oldGenres, actionKeyGenres);
            var (newTracks, oldTracks, actionKeyTracks) = SelectedNomenclature.CompareNewAndOldTracks(_refDatabase!.Nomenclatures![i]);
            UpdateTracks(newTracks, oldTracks, actionKeyTracks);
            var (idRecord, newTitle, newDuration, newPublisher, newYear, newFormat, newCover) = SelectedNomenclature.GetFieldsToUpdate();
            UpdateNomenclatureRecord(idRecord, newTitle, newDuration, newPublisher, newYear, newFormat, newCover);
            UpdateNomenclaturePrice(SelectedNomenclature.GetSelectedId(), SelectedNomenclature.GetPrice());
            ClearFunction();
            _refDatabase.RefreshNomenclaturesAndLists();
        }

        private void ClearFunction(string? error = null)
        {
            SelectedNomenclature!.Clear(error);
            CanChangeId = true;
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
                foreach (var track in newTracks)
                {
                    if (i == trackIds.Count) break;
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
                    _refDb.DeletePerformerItem(bandItemIds[i]);
                }
            }
            else
            {
                int i = 0;
                foreach (var band in newBands)
                {
                    if (i == bandItemIds.Count) break;
                    _refDb.UpdateBandItem(bandItemIds[i], band);
                    i++;
                }
                for (; i < newBands.Count; i++)
                {
                    _refDb.AddBandItem(idRecord, newBands[i]);
                }
            }
        }

        private void UpdatePerformerItems(List<(string, string)> newPerformers, List<(string, string)> oldPerformers, string actionKey)
        {
            var idRecord = SelectedNomenclature!.GetSelectedId();
            var performerItemIds = _refDb.GetAllPerformerItemIds(idRecord);

            if (actionKey == "update")
            {
                int i = 0;
                foreach (var id in performerItemIds)
                {
                    _refDb.UpdatePerformerItem(id, newPerformers[i].Item1, newPerformers[i].Item2);
                    i++;
                }
            }
            else if (actionKey == "delete")
            {
                int i = 0;
                foreach (var performer in newPerformers)
                {
                    _refDb.UpdatePerformerItem(performerItemIds[i], performer.Item1, performer.Item2);
                    i++;
                }
                for (; i < oldPerformers.Count; i++)
                {
                    _refDb.DeletePerformerItem(performerItemIds[i]);
                }
            }
            else
            {
                int i = 0;
                foreach (var performer in newPerformers)
                {
                    if (i == performerItemIds.Count) break;
                    _refDb.UpdatePerformerItem(performerItemIds[i], performer.Item1, performer.Item2);
                    i++;
                }
                for (; i < newPerformers.Count; i++)
                {
                    _refDb.AddPerformerItem(idRecord, newPerformers[i].Item1, newPerformers[i].Item2);
                }
            }
        }

        private void UpdateGenreItems(List<string> newGenres, List<string> oldGenres, string actionKey)
        {
            var idRecord = SelectedNomenclature!.GetSelectedId();
            var genreItemIds = _refDb.GetAllGenreItemIds(idRecord);

            if (actionKey == "update")
            {
                int i = 0;
                foreach (var id in genreItemIds)
                {
                    _refDb.UpdateGenreItem(id, newGenres[i]);
                    i++;
                }
            }
            else if (actionKey == "delete")
            {
                int i = 0;
                foreach (var genre in newGenres)
                {
                    _refDb.UpdateGenreItem(genreItemIds[i], genre);
                    i++;
                }
                for (; i < oldGenres.Count; i++)
                {
                    _refDb.DeleteGenreItem(genreItemIds[i]);
                }
            }
            else
            {
                int i = 0;
                foreach (var genre in newGenres)
                {
                    if (i == genreItemIds.Count) break;
                    _refDb.UpdateGenreItem(genreItemIds[i], genre);
                    i++;
                }
                for (; i < newGenres.Count; i++)
                {
                    _refDb.AddGenreItem(idRecord, newGenres[i]);
                }
            }
        }

        private void UpdateNomenclatureRecord(int idRecord, string newTitle, string newDuration, string newPublisher, string newYear, string newFormat, string newCover)
        {
            _refDb.UpdateRecord(idRecord, newTitle, newDuration, newPublisher, newYear, newFormat, newCover);
        }

        private void UpdateNomenclaturePrice(int idRecord, double price)
        {
            _refDb.UpdatePrice(idRecord, price);
        }

        public void RefreshLanguage(ref LanguagePack language)
        {
            _language = language;
        }

        public void OpenNomenclatureFromDbInEditor(int? nomenclatureId = null)
        {
            if (nomenclatureId is not null) SelectedNomenclature!.SelectedId = nomenclatureId.ToString();
            FindNomenclatureFunction();
        }
    }
}
