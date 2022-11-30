using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NazarTunes.ViewModels.AdminLayer.Tab2
{
    public class TabNewNomenclature : Notifier
    {
        private readonly AdminLayerDb _refDb;
        private readonly Database _refDatabase;

        public ObservableCollection<string>? BandsFromDb { get; set; }
        private string? _selectedBand;
        public string? SelectedBand
        {
            get => _selectedBand;
            set
            {
                SetField(ref _selectedBand, value);
            }
        }

        public ObservableCollection<string>? PerformersFromDb { get; set; }
        private string? _selectedPerformer;
        public string? SelectedPerformer
        {
            get => _selectedPerformer;
            set
            {
                SetField(ref _selectedPerformer, value);
            }
        }

        public ObservableCollection<string>? GenresFromDb { get; set; }
        private string? _selectedGenre;
        public string? SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                SetField(ref _selectedGenre, value);
            }
        }

        private NomenclatureCreator? _newNomenclature;
        public NomenclatureCreator? NewNomenclature
        {
            get => _newNomenclature;
            set => SetField(ref _newNomenclature, value);
        }

        public MyCommand CommandAddBand { get; }
        public MyCommand CommandAddPerformer { get; }
        public MyCommand CommandAddGenre { get; }
        public MyCommand CommandClear { get; }
        public MyCommand CommandSave { get; }

        public TabNewNomenclature(ref AdminLayerDb db, ref Database database)
        {
            _refDb = db;
            BandsFromDb = new(GetBands(database.Bands!));
            PerformersFromDb = new(GetPerformers(database.Performers!));
            GenresFromDb = new(GetGenres(database.Genres!));
            _refDatabase = database;

            NewNomenclature = new();

            CommandAddBand = new(_ =>
            {
                AddBandFunction();
            }, _ => true);
            CommandAddPerformer = new(_ =>
            {
                AddPerformerFunction();
            }, _ => true);
            CommandAddGenre = new(_ =>
            {
                AddGenreFunction();
            }, _ => true);
            CommandSave = new(_ =>
            {
                SaveFunction();
            }, _ => true);
            CommandClear = new(_ =>
            {
                ClearFunction();
            }, _ => true);
        }

        private void AddBandFunction()
        {
            if (SelectedBand is not null)
            {
                NewNomenclature!.AddBand(SelectedBand!);
                BandsFromDb!.Remove(SelectedBand!);
            }
        }

        private void AddPerformerFunction()
        {
            if (SelectedPerformer is not null)
            {
                NewNomenclature!.AddPerformer(SelectedPerformer!);
                PerformersFromDb!.Remove(SelectedPerformer!);
            }
        }
        private void AddGenreFunction()
        {
            if (SelectedGenre is not null)
            {
                NewNomenclature!.AddGenre(SelectedGenre!);
                GenresFromDb!.Remove(SelectedGenre!);
            }
        }

        private List<string> GetBands(List<Band> bands)
        {
            var list = new List<string>();
            foreach (var band in bands)
            {
                list.Add(band.ToString());
            }
            return list;
        }
        private List<string> GetPerformers(List<Performer> performers)
        {
            var list = new List<string>();
            foreach (var performer in performers)
            {
                list.Add(performer.ToString());
            }
            return list;
        }
        private List<string> GetGenres(List<Genre> genres)
        {
            var list = new List<string>();
            foreach (var genre in genres)
            {
                list.Add(genre.ToString());
            }
            return list;
        }

        private void ClearFunction()
        {
            NewNomenclature!.Clear();
            RefreshListsFromDb();
        }

        private void SaveFunction()
        {
            var (newTitle, newDuration, newPublisher, newYear, newFormat, newCover) = NewNomenclature!.GetFieldsToCreate();
            var id = _refDb.CreateNewNomenclatureAndGetId(newTitle, newDuration, newPublisher, newYear, newFormat, newCover);
            CreatePerformerItems(id, NewNomenclature.GetPerformers());
            CreateBandItems(id, NewNomenclature.GetBands());
            CreateGenreItems(id, NewNomenclature.GetGenres());
            CreateTracks(id, NewNomenclature.GetTracks());
            ClearFunction();
            RefreshListsFromDb();
            _refDatabase.RefreshNomenclaturesAndLists();
        }

        private void CreatePerformerItems(int id, List<(string, string)> performers)
        {
            foreach (var performer in performers)
            {
                _refDb.AddPerformerItem(id, performer.Item1, performer.Item2);
            }
        }

        private void CreateBandItems(int id, List<string> bands)
        {
            foreach (var band in bands)
            {
                _refDb.AddBandItem(id, band);
            }
        }

        private void CreateGenreItems(int id, List<string> genres)
        {
            foreach (var genre in genres)
            {
                _refDb.AddGenreItem(id, genre);
            }
        }

        private void CreateTracks(int id, List<string> tracks)
        {
            foreach (var track in tracks)
            {
                _refDb.AddOneTrack(id, track);
            }
        }

        private void RefreshListsFromDb()
        {
            SelectedBand = null;
            SelectedPerformer = null;
            SelectedGenre = null;

            BandsFromDb!.Clear();
            var bands = GetBands(_refDatabase.Bands!);
            foreach (var b in bands)
            {
                BandsFromDb!.Add(b);
            }
            PerformersFromDb!.Clear();
            var performers = GetPerformers(_refDatabase.Performers!);
            foreach (var p in performers)
            {
                PerformersFromDb!.Add(p);
            }
            GenresFromDb!.Clear();
            var genres = GetGenres(_refDatabase.Genres!);
            foreach (var g in genres)
            {
                GenresFromDb!.Add(g);
            }
        }
    }
}
