using Dapper;
using MySql.Data.MySqlClient;
using NazarTunes.Models.DataTemplates;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
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

            _cmd.Parameters.AddWithValue("id_performer", id);
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

        public List<Procurement> GetAllProcurements()
        {
            var sql = $"CALL procedure_get_all_procurements();";
            _db.Open();
            var list = _db.Query<Procurement>(sql).ToList();
            _db.Close();
            return list;
        }

        public void AddProcurement(int recordId, int supplierId, DateTime date, int amount, double price)
        {
            _cmd.CommandText = "procedure_create_new_procurement";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("new_date", date);
            _cmd.Parameters.AddWithValue("new_supplier_id", supplierId);
            _cmd.Parameters.AddWithValue("new_record_id", recordId);
            _cmd.Parameters.AddWithValue("new_amount", amount);
            _cmd.Parameters.AddWithValue("new_cost_price", price);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        private List<Promotion> GetPromotionsByGenre()
        {
            var sql = $"CALL procedure_get_all_promotions_by_genre();";
            _db.Open();
            var list = _db.Query<Promotion>(sql).ToList();
            _db.Close();
            return list;
        }

        private List<Promotion> GetPromotionsByBand()
        {
            var sql = $"CALL procedure_get_all_promotions_by_band();";
            _db.Open();
            var list = _db.Query<Promotion>(sql).ToList();
            _db.Close();
            return list;
        }

        private List<Promotion> GetPromotionsByPerformer()
        {
            var sql = $"CALL procedure_get_all_promotions_by_performer();";
            _db.Open();
            var list = _db.Query<Promotion>(sql).ToList();
            _db.Close();
            return list;
        }

        private List<Promotion> GetPromotionsByRecord()
        {
            var sql = $"CALL procedure_get_all_promotions_by_record();";
            _db.Open();
            var list = _db.Query<Promotion>(sql).ToList();
            _db.Close();
            return list;
        }

        public List<Promotion> GetPromotions()
        {
            var list = new List<Promotion>();
            list.AddRange(GetPromotionsByGenre());
            list.AddRange(GetPromotionsByBand());
            list.AddRange(GetPromotionsByPerformer());
            list.AddRange(GetPromotionsByRecord());
            return list.OrderBy(p => p.DiscountPromotionId).ToList();
        }

        public void AddGenrePromo(int genreId, int discount, bool isStarted)
        {
            _cmd.CommandText = "procedure_create_new_promotion_by_genre";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_genre", genreId);
            _cmd.Parameters.AddWithValue("new_discount", discount);
            _cmd.Parameters.AddWithValue("new_is_started", isStarted);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddBandPromo(int bandId, int discount, bool isStarted)
        {
            _cmd.CommandText = "procedure_create_new_promotion_by_band";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_band", bandId);
            _cmd.Parameters.AddWithValue("new_discount", discount);
            _cmd.Parameters.AddWithValue("new_is_started", isStarted);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddPerformerPromo(int performerId, int discount, bool isStarted)
        {
            _cmd.CommandText = "procedure_create_new_promotion_by_performer";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_performer", performerId);
            _cmd.Parameters.AddWithValue("new_discount", discount);
            _cmd.Parameters.AddWithValue("new_is_started", isStarted);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void AddRecordPromo(int recordId, int discount, bool isStarted)
        {
            _cmd.CommandText = "procedure_create_new_promotion_by_record";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_record", recordId);
            _cmd.Parameters.AddWithValue("new_discount", discount);
            _cmd.Parameters.AddWithValue("new_is_started", isStarted);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void StartPromotion(int promoId)
        {
            _cmd.CommandText = "procedure_start_promotion";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("promotion_id", promoId);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public void FinishPromotion(int promoId)
        {
            _cmd.CommandText = "procedure_finish_promotion";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("promotion_id", promoId);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public bool CheckIfClientExists(int idClient, string clientsPhone, string clientsEmail)
        {
            _cmd.CommandText = "function_check_if_client_exists";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_client", idClient);
            _cmd.Parameters.AddWithValue("clients_phone", clientsPhone);
            _cmd.Parameters.AddWithValue("clients_email", clientsEmail);

            var exists = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            exists.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)exists.Value == 1;
        }

        public (int id, string clientsFullName) GetClientsFullNameAndId(int idClient, string clientsPhone, string clientsEmail)
        {
            _cmd.CommandText = "procedure_get_clients_full_name_and_id";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_client", idClient);
            _cmd.Parameters.AddWithValue("clients_phone", clientsPhone);
            _cmd.Parameters.AddWithValue("clients_email", clientsEmail);

            var getId = _cmd.Parameters.Add("id", MySqlDbType.Int32);
            getId.Direction = ParameterDirection.Output;
            var getFirstName = _cmd.Parameters.Add("clients_first_name", MySqlDbType.VarChar);
            getFirstName.Direction = ParameterDirection.Output;
            var getLastName = _cmd.Parameters.Add("clients_last_name", MySqlDbType.VarChar);
            getLastName.Direction = ParameterDirection.Output;

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();

            return ((int)getId.Value, $"{(string)getFirstName.Value} {(string)getLastName.Value}");
        }

        public bool CheckIfEnteredAmountExceedsActual(int idNomenclature, int amount)
        {
            _cmd.CommandText = "function_check_if_entered_amount_exceeds_actual";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_nomenclature", idNomenclature);
            _cmd.Parameters.AddWithValue("entered_amount", amount);

            var exceeds = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            exceeds.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)exceeds.Value == 1;
        }

        public void AddNewFrozenItem(int idNomenclature,int idClient, int amount)
        {
            _cmd.CommandText = "procedure_add_new_frozen_item";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();

            _cmd.Parameters.AddWithValue("id_nomenclature", idNomenclature);
            _cmd.Parameters.AddWithValue("id_client", idClient);
            _cmd.Parameters.AddWithValue("new_amount", amount);

            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }
    }
}
