using Dapper;
using NazarTunes.Models.DataTemplates;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NazarTunes.Models.MySQLConnections
{
    public class AdminLayerDb : CommonDb
    {
        public IEnumerable<Band> GetAllBands()
        {
            var sql = "CALL procedure_get_all_bands();";
            _db.Open();
            var list = _db.Query<Band>(sql).ToList();
            _db.Close();
            var sorted_list = list.OrderBy(b => b.BandName).ToList();
            return sorted_list;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var sql = "CALL procedure_get_all_genres();";
            _db.Open();
            var list = _db.Query<Genre>(sql).ToList();
            _db.Close();
            var sorted_list = list.OrderBy(g => g.GenreName).ToList();
            return sorted_list;
        }

        public IEnumerable<Performer> GetAllPerformers()
        {
            var sql = "CALL procedure_get_all_performers();";
            _db.Open();
            var list = _db.Query<Performer>(sql).ToList();
            _db.Close();
            var sorted_list = list.OrderBy(p => p.FirstName).ToList();
            return sorted_list;
        }

        public void UpdatePerformer(int id, string newFirstName, string newLastName)
        {
            _cmd.CommandText = "procedure_update_person_performer";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id", id);
            _cmd.Parameters.AddWithValue("new_first_name", newFirstName);
            _cmd.Parameters.AddWithValue("new_last_name", newLastName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void UpdateBand(int id, string newBandName)
        {
            _cmd.CommandText = "procedure_update_band";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id", id);
            _cmd.Parameters.AddWithValue("new_band_name", newBandName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void UpdateGenre(int id, string newGenreName)
        {
            _cmd.CommandText = "procedure_update_genre";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id", id);
            _cmd.Parameters.AddWithValue("new_genre_name", newGenreName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }




        //public void AddNewSupplier()
        //{

        //}

        //public List<Supplier> GetAllSuppliers()
        //{

        //}

        //public void AddNewRecord()
        //{

        //}


    }
}
