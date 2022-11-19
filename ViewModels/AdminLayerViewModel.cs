﻿using NazarTunes.Models.DataTemplates;
using NazarTunes.Models.MySQLConnections;
using NazarTunes.ViewModels.Commands;
using NazarTunes.ViewModels.Notifiers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Documents;
using System.Linq;

namespace NazarTunes.ViewModels
{
    public class AdminLayerViewModel : Notifier
    {
        private readonly AdminLayerDb _db;

        private Admin? _user;
        public Admin? User 
        { 
            get => _user;
            set => SetField(ref _user, value);
        }

        private ObservableCollection<Nomenclature>? _nomenclatures;
        public ObservableCollection<Nomenclature>? Nomenclatures
        {
            get => _nomenclatures;
            set => SetField(ref _nomenclatures, value);
        }

        private NomenclatureConstructor? _selectedNomenclature;
        public NomenclatureConstructor SelectedNomenclature
        {
            get => _selectedNomenclature;
            set => SetField(ref _selectedNomenclature, value);
        }



        public int SelectedTab { get; set; }
        public string? SelectedId { get; set; }

        public MyCommand CommandFindNomenclature { get; }
        public MyCommand CommandSaveChanges { get; }

        public AdminLayerViewModel (Admin admin)
        {
            _db = new ();
            User = admin;
            Nomenclatures = new ObservableCollection<Nomenclature>(_db.GetAllNomenclatures());
            SelectedTab = 0;
            SelectedNomenclature = new();
            CommandFindNomenclature = new(_ =>
            {
                FindNomenclatureFunction();
            }, _ => true);
            CommandSaveChanges = new(_ =>
            {
                SaveChangesFunction();
            }, _ => true);
        }

        private List<string> MakeList(string str)
        {
            if (str[^1] != '\n') str += "\r\n";
            var list = new List<string>();
            while (str != string.Empty)
            {
                var tmp = str.Substring(0, str.IndexOf('\n')-1);
                str = str.Remove(0, str.IndexOf('\n') + 1);
                list.Add(tmp);
            }
            return list;
        }

        private string MakeColumn(List<string> list)
        {
            var str = new StringBuilder();
            var stop = list.Count - 1;
            foreach (var item in list)
            {
                if (list.IndexOf(item) == stop) { str.Append(item); break; }
                str.Append(item + '\n');
            }
            return str.ToString();
        }
        private bool ContainsOnlyDigits(string str)
        {
            var result = true;
            foreach (var symbol in str)
            {
                if (!char.IsDigit(symbol)) { result = false; break; }
            }
            return result;
        }


        private void FindNomenclatureFunction()  
        {           
            if (SelectedId is not null)
            {
                if (ContainsOnlyDigits(SelectedId))
                {
                    var i = int.Parse(SelectedId!) - 1;
                    if (i <= Nomenclatures!.Count - 1 && i >= 0)
                    {
                        SelectedNomenclature.Title = Nomenclatures![i].Record!.Title;
                        SelectedNomenclature.Bands = MakeColumn(Nomenclatures![i].Record!.Bands!);
                        SelectedNomenclature.Performers = MakeColumn(Nomenclatures![i].Record!.Performers!);
                        SelectedNomenclature.Genres = MakeColumn(Nomenclatures![i].Record!.Genres!);
                        SelectedNomenclature.Tracks = MakeColumn(Nomenclatures![i].Record!.Tracks!);
                        SelectedNomenclature.TotalDuration = Nomenclatures![i].Record!.TotalDuration;
                        SelectedNomenclature.Publisher = Nomenclatures![i].Record!.Publisher;
                        SelectedNomenclature.ReleaseYear = Nomenclatures![i].Record!.ReleaseYear;
                        SelectedNomenclature.MediaFormat = Nomenclatures![i].Record!.MediaFormat;
                        SelectedNomenclature.CoverPath = Nomenclatures![i].Record!.CoverPath;
                        SelectedNomenclature.SellPrice = Nomenclatures![i].SellPrice.ToString();
                        SelectedNomenclature.HelperText = string.Empty;
                    }
                    else SelectedNomenclature.HelperText = "Invalid ID!";
                }
                else SelectedNomenclature.HelperText = "This field may contain only digits!";
            }
            else SelectedNomenclature.HelperText = "Enter ID!";
        }

        private void SaveChangesFunction()
        {
            var result = CompareNewAndOldRecordBands();

            //TODO finish this function with all list fields!!!






        }

        private (List<string> bandsToCreate, List<string> bandsToDelete) CompareNewAndOldRecordBands()
        {
            var newBands = MakeList(SelectedNomenclature.Bands!);
            var oldBands = new List<string>(Nomenclatures![int.Parse(SelectedId!) - 1].Record!.Bands!);
            var matches = (from item in newBands
                           where oldBands.Contains(item)
                           select item).ToList();
            if (matches.Count>0)
            {
                foreach (var item in matches)
                {
                    newBands.Remove(item);
                    oldBands.Remove(item);
                }
            }
            return (newBands, oldBands);
        }
    }
}
