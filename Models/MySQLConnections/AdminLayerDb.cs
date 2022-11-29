using Dapper;
using MySql.Data.MySqlClient;
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

        public List<int> GetAllTrackIds(int id_record)
        {
            var sql = $"CALL procedure_get_all_track_ids_for_one_record({id_record});";
            _db.Open();
            var list = _db.Query<int>(sql).ToList();
            _db.Close();
            return list;
        }

        public void UpdateOneTrack(int id, string newTrackTitle)
        {
            _cmd.CommandText = "procedure_update_track";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_track", id);
            _cmd.Parameters.AddWithValue("new_track_title", newTrackTitle);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void DeleteOneTrack(int id)
        {
            _cmd.CommandText = "procedure_delete_track";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_track", id);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddOneTrack(int idRecord, string newTrackTitle)
        {
            _cmd.CommandText = "procedure_add_new_track";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_record", idRecord);
            _cmd.Parameters.AddWithValue("new_track_title", newTrackTitle);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public List<int> GetAllBandItemIds(int id_record)
        {
            var sql = $"CALL procedure_get_all_record_performer_item_with_band_ids({id_record});";
            _db.Open();
            var list = _db.Query<int>(sql).ToList();
            _db.Close();
            return list;
        }


        public void UpdateBandItem(int id, string newBandName)
        {
            _cmd.CommandText = "procedure_update_record_performer_item_with_band";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("item_id", id);
            _cmd.Parameters.AddWithValue("new_band_name", newBandName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddBandItem(int idRecord, string newBandName)
        {
            _cmd.CommandText = "procedure_create_record_performer_item_with_band";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_record_id", idRecord);
            _cmd.Parameters.AddWithValue("new_band_name", newBandName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public List<int> GetAllPerformerItemIds(int id_record)
        {
            var sql = $"CALL procedure_get_all_record_performer_item_with_performer_ids({id_record});";
            _db.Open();
            var list = _db.Query<int>(sql).ToList();
            _db.Close();
            return list;
        }

        public void UpdatePerformerItem(int id, string firstName, string lastName)
        {
            _cmd.CommandText = "procedure_update_record_performer_item_with_performer";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("item_id", id);
            _cmd.Parameters.AddWithValue("new_first_name", firstName);
            _cmd.Parameters.AddWithValue("new_last_name", lastName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddPerformerItem(int idRecord, string firstName, string lastName)
        {
            _cmd.CommandText = "procedure_create_record_performer_item_with_performer";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_record_id", idRecord);
            _cmd.Parameters.AddWithValue("new_first_name", firstName);
            _cmd.Parameters.AddWithValue("new_last_name", lastName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void DeletePerformerItem(int id)
        {
            _cmd.CommandText = "procedure_delete_record_performer_item";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("item_id", id);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public List<int> GetAllGenreItemIds(int id_record)
        {
            var sql = $"CALL procedure_get_all_record_genre_item_ids({id_record});";
            _db.Open();
            var list = _db.Query<int>(sql).ToList();
            _db.Close();
            return list;
        }

        public void UpdateGenreItem(int id, string newGenreName)
        {
            _cmd.CommandText = "procedure_update_record_genre_item";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("item_id", id);
            _cmd.Parameters.AddWithValue("new_genre_name", newGenreName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddGenreItem(int idRecord, string newGenreName)
        {
            _cmd.CommandText = "procedure_create_record_genre_item";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_record_id", idRecord);
            _cmd.Parameters.AddWithValue("new_genre_name", newGenreName);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void DeleteGenreItem(int id)
        {
            _cmd.CommandText = "procedure_delete_record_genre_item";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("item_id", id);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void UpdateRecord(int idRecord, string newTitle, string newDuration, string newPublisher, string newYear, string newFormat, string newCover)
        {
            _cmd.CommandText = "procedure_update_record";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_record", idRecord);
            _cmd.Parameters.AddWithValue("new_title", newTitle);
            _cmd.Parameters.AddWithValue("new_total_duration", newDuration);
            _cmd.Parameters.AddWithValue("new_publisher", newPublisher);
            _cmd.Parameters.AddWithValue("new_release_year", newYear);
            _cmd.Parameters.AddWithValue("new_media_format", newFormat);
            _cmd.Parameters.AddWithValue("new_cover_path", newCover);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void UpdatePrice(int id, double price)
        {
            _cmd.CommandText = "procedure_set_nomenclature_sell_price";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("nom_id", id);
            _cmd.Parameters.AddWithValue("new_price", price);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public int CreateNewNomenclatureAndGetId(string newTitle, string newDuration, string newPublisher, string newYear, string newFormat, string newCover)
        {
            _cmd.CommandText = "procedure_create_record_and_get_id";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_title", newTitle);
            _cmd.Parameters.AddWithValue("new_total_duration", newDuration);
            _cmd.Parameters.AddWithValue("new_publisher", newPublisher);
            _cmd.Parameters.AddWithValue("new_release_date", newYear);
            _cmd.Parameters.AddWithValue("new_media_format", newFormat);
            _cmd.Parameters.AddWithValue("new_cover_path", newCover);

            var getId = _cmd.Parameters.Add("new_record_id", MySqlDbType.Int32);
            getId.Direction = ParameterDirection.Output;

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();

            return (int)getId.Value;
        }

        public List<Supplier> GetAllSuppliers()
        {
            var sql = $"CALL procedure_get_all_suppliers();";
            _db.Open();
            var list = _db.Query<Supplier>(sql).ToList();
            _db.Close();
            return list;
        }

        public void UpdateSupplier(int id, string newSupplierName, string newContactInfo, bool cooperating)
        {
            _cmd.CommandText = "procedure_update_supplier";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("id_supplier", id);
            _cmd.Parameters.AddWithValue("new_supplier", newSupplierName);
            _cmd.Parameters.AddWithValue("new_contact_info", newContactInfo);
            _cmd.Parameters.AddWithValue("cooperating", cooperating);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddSupplier(string newSupplierName, string newContactInfo)
        {
            _cmd.CommandText = "procedure_create_supplier";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_supplier", newSupplierName);
            _cmd.Parameters.AddWithValue("new_contact_info", newContactInfo);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        //public void AddNewSupplier()
        //{

        //} 


    }
}
