using MySql.Data.MySqlClient;
using NazarTunes.Models.DataTemplates;
using System.Collections.Generic;
using System.Data;

namespace NazarTunes.Models.MySQLConnections;

public class Db : AbsctractDb
{

    

    private bool IfCredentialsCorrect(string login, string password)
    {
        _cmd.CommandText = "function_check_if_credentials_correct";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Clear();
        _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
        _cmd.Parameters["new_login"].Value = login;
        _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
        _cmd.Parameters["new_pass"].Value = password;
        var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        _db.Open();
        _cmd.ExecuteNonQuery();
        _db.Close();
        return (int)returnParameter.Value == 1;
    }

    private bool IfUserIsDeleted(string login, string password)
    {
        _cmd.CommandText = "function_check_if_user_is_deleted";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Clear();
        _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
        _cmd.Parameters["new_login"].Value = login;
        _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
        _cmd.Parameters["new_pass"].Value = password;
        var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        _db.Open();
        _cmd.ExecuteNonQuery();
        _db.Close();
        return (int)returnParameter.Value == 1;
    }

    public (bool correct_credentials, bool deleted_user, AbstractUser? user) Authorization(string login, string password)
    {
        if (IfCredentialsCorrect(login, password))
        {
            if (!IfUserIsDeleted(login, password))
            {
                var role = GetRole(login, password);
                if (role == "admin")
                {
                    return (true, false, GetAdmin(login, password, role));
                }
                else
                {
                    return (true, false, GetClient(login, password, role));
                }
            }
            else return (true, true, null);
        }
        return (false, true, null);
    }

    private Admin GetAdmin(string login, string password, string role)
    {
        _cmd.CommandText = "procedure_get_admin";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Clear();
        _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
        _cmd.Parameters["new_login"].Value = login;
        _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
        _cmd.Parameters["new_pass"].Value = password;
        var getId = _cmd.Parameters.Add("user_id", MySqlDbType.Int32);
        getId.Direction = ParameterDirection.Output;
        var getFirstName = _cmd.Parameters.Add("user_first_name", MySqlDbType.VarChar);
        getFirstName.Direction = ParameterDirection.Output;
        var getLastName = _cmd.Parameters.Add("user_last_name", MySqlDbType.VarChar);
        getLastName.Direction = ParameterDirection.Output;
        _db.Open();
        _cmd.ExecuteNonQuery();
        _db.Close();
        return new Admin { Id = (int)getId.Value, FirstName = (string)getFirstName.Value, LastName = (string)getLastName.Value, Role = role };
    }

    private Client GetClient(string login, string password, string role)
    {
        _cmd.CommandText = "procedure_get_client";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Clear();
        _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
        _cmd.Parameters["new_login"].Value = login;
        _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
        _cmd.Parameters["new_pass"].Value = password;
        var getId = _cmd.Parameters.Add("user_id", MySqlDbType.Int32);
        getId.Direction = ParameterDirection.Output;
        var getFirstName = _cmd.Parameters.Add("user_first_name", MySqlDbType.VarChar);
        getFirstName.Direction = ParameterDirection.Output;
        var getLastName = _cmd.Parameters.Add("user_last_name", MySqlDbType.VarChar);
        getLastName.Direction = ParameterDirection.Output;
        var getPhone = _cmd.Parameters.Add("user_phone", MySqlDbType.VarChar);
        getPhone.Direction = ParameterDirection.Output;
        var getEmail = _cmd.Parameters.Add("user_email", MySqlDbType.VarChar);
        getEmail.Direction = ParameterDirection.Output;
        var getTotalAmount = _cmd.Parameters.Add("user_total_amount_spent", MySqlDbType.Double);
        getTotalAmount.Direction = ParameterDirection.Output;
        var getDiscount = _cmd.Parameters.Add("user_personal_discount", MySqlDbType.Int32);
        getDiscount.Direction = ParameterDirection.Output;
        var getIsSubscribed = _cmd.Parameters.Add("user_is_subscribed", MySqlDbType.Int32);
        getIsSubscribed.Direction = ParameterDirection.Output;
        _db.Open();
        _cmd.ExecuteNonQuery();
        _db.Close();
        return new Client
        {
            Id = (int)getId.Value,
            FirstName = (string)getFirstName.Value,
            LastName = (string)getLastName.Value,
            Role = role,
            Phone = (string)getPhone.Value,
            Email = (string)getEmail.Value,
            TotalAmountSpent = (double)getTotalAmount.Value,
            PersonalDiscount = (int)getDiscount.Value,
            IsSubscribed = (int)getIsSubscribed.Value == 1
        };
    }


    private string GetRole(string login, string password)
    {
        _cmd.CommandText = "function_get_role";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Clear();
        _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
        _cmd.Parameters["new_login"].Value = login;
        _cmd.Parameters.Add("new_pass", MySqlDbType.VarChar);
        _cmd.Parameters["new_pass"].Value = password;
        var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.VarChar);
        returnParameter.Direction = ParameterDirection.ReturnValue;
        _db.Open();
        _cmd.ExecuteNonQuery();
        _db.Close();
        return (string)returnParameter.Value;
    }
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
                list_genres.Add(result.GetString("genre"));
            }
        }
        _db.Close();
        return list_genres;
    }

    private List<string> GetRecordPerformers(int record_id)
    {
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
                list_performers.Add(result1.GetString("band_name"));
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

        return list_performers;
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
            Performers = performers,
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
        return (int)returnParameter.Value;
    }


    public List<Record> GetAllRecords()
    {
        var count = GetRecordsCount();
        var records = new List<Record>();
        for (int i = 1; i <= count; i++)
        {
            records.Add(GetOneRecord(i));
        }
        return records;
    }
}
