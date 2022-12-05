using Dapper;
using MySql.Data.MySqlClient;
using NazarTunes.Models.DataTemplates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NazarTunes.Models.MySQLConnections
{
    public abstract class CommonDb : AbsctractDb
    {
        private List<string> GetRecordGenres(int record_id)
        {
            var list_genres = new List<string>();
            _cmd.CommandText = $"CALL procedure_get_record_genres ({record_id})";
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.Clear();
            _db.Open();
            var result = _cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    list_genres.Add(result.GetString("genre_name"));
                }
            }
            _db.Close();
            return list_genres;
        }

        private (List<string> bands, List<string> performers) GetRecordPerformers(int record_id)
        {
            var list_bands = new List<string>();
            var list_performers = new List<string>();
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.Clear();
            _cmd.CommandText = $"CALL procedure_get_record_performers_bands ({record_id})";
            _db.Open();
            var result1 = _cmd.ExecuteReader();
            if (result1.HasRows)
            {
                while (result1.Read())
                {
                    list_bands.Add(result1.GetString("band_name"));
                }
            }
            _db.Close();

            _cmd.CommandText = $"CALL procedure_get_record_performers_persons ({record_id})";
            _db.Open();
            var result2 = _cmd.ExecuteReader();
            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    var Name = result2.GetString("first_name") + " " + result2.GetString("last_name");
                    list_performers.Add(Name);
                }
            }
            _db.Close();

            return (list_bands, list_performers);
        }

        private List<string> GetRecordTracks(int record_id)
        {
            var list_tracks = new List<string>();
            _cmd.CommandType = CommandType.Text;
            _cmd.Parameters.Clear();
            _cmd.CommandText = $"CALL procedure_get_record_tracks ({record_id})";
            _db.Open();
            var result = _cmd.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    list_tracks.Add(result.GetString("track_title"));
                }
            }
            _db.Close();
            return list_tracks;
        }

        private Record GetOneRecord(int record_id)
        {
            _cmd.CommandText = "procedure_get_record";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("id_record", MySqlDbType.Int32);
            _cmd.Parameters["id_record"].Value = record_id;

            var getTitle = _cmd.Parameters.Add("rec_title", MySqlDbType.VarChar);
            getTitle.Direction = ParameterDirection.Output;

            var getDuration = _cmd.Parameters.Add("rec_total_duration", MySqlDbType.Time);
            getDuration.Direction = ParameterDirection.Output;

            var getPublisher = _cmd.Parameters.Add("rec_publisher", MySqlDbType.VarChar);
            getPublisher.Direction = ParameterDirection.Output;

            var getYear = _cmd.Parameters.Add("rec_release_year", MySqlDbType.VarChar);
            getYear.Direction = ParameterDirection.Output;

            var getFormat = _cmd.Parameters.Add("rec_media_format", MySqlDbType.VarChar);
            getFormat.Direction = ParameterDirection.Output;

            var getCover = _cmd.Parameters.Add("rec_cover_path", MySqlDbType.VarChar);
            getCover.Direction = ParameterDirection.Output;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            var genres = GetRecordGenres(record_id);
            var performers = GetRecordPerformers(record_id);
            var tracks = GetRecordTracks(record_id);
            return new Record
            {
                Id = record_id,
                Title = (string)getTitle.Value,
                TrackAmount = tracks.Count,
                Tracks = tracks,
                Genres = genres,
                Performers = performers.performers,
                Bands = performers.bands,
                TotalDuration = getDuration.Value.ToString()!.Substring(3),
                Publisher = (string)getPublisher.Value,
                ReleaseYear = (string)getYear.Value,
                MediaFormat = (string)getFormat.Value,
                CoverPath = (string)getCover.Value
            };
        }

        private int GetRecordsCount()
        {
            _cmd.CommandText = "function_get_all_record_ids";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Clear();
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return returnParameter.Value is DBNull ? 0 : (int)returnParameter.Value;
        }

        private List<Record> GetAllRecords()
        {
            var count = GetRecordsCount();
            var records = new List<Record>();
            for (int i = 1; i <= count; i++)
            {
                records.Add(GetOneRecord(i));
                records[i - 1].BandsToString = MakeString(records[i - 1].Bands!);
                records[i - 1].PerformersToString = MakeString(records[i - 1].Performers!);
                records[i - 1].GenresToString = MakeString(records[i - 1].Genres!);
            }
            return records;
        }

        public IEnumerable<Nomenclature> GetAllNomenclatures()
        {
            var sql = "CALL procedure_get_all_nomenclatures();";
            _db.Open();
            var list = _db.Query<Nomenclature>(sql);
            _db.Close();
            var records = GetAllRecords();
            int i = 0;
            foreach (var item in list)
            {
                item.Record = records[i];
                i++;
            }
            return list;
        }

        private string MakeString(List<string> list)
        {
            var str = new StringBuilder();
            var stop = list.Count - 1;
            foreach (var item in list)
            {
                if (list.IndexOf(item) == stop) { str.Append(item); break; }
                str.Append(item + ", ");
            }
            return str.ToString();
        }
    }
}
