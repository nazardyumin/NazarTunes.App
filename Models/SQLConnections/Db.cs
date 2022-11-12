using Dapper;
using MySql.Data.MySqlClient;
using NazarTunes.Models.Configurators;
using System.Data;

namespace NazarTunes.Models.SQLConnections
{
    public class Db
    {
        private readonly MySqlConnection _db;
        private readonly MySqlCommand _cmd;


        public Db()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _db = new MySqlConnection(DbConfig.GetConnectionString("db.txt"));
            _cmd = new MySqlCommand { Connection = _db };
        }

        public void test(string login, string pass, int role, string fn, string ln)
        {
            _cmd.CommandText =
            $"CALL procedure_create_user_test('{login}', '{pass}',{role},'{fn}','{ln}')";
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
        }

        public bool IfLoginExists(string login)
        {
            _cmd.CommandText = "function_check_if_login_exists";
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)returnParameter.Value == 1;
        }

        public bool IfCredentialsCorrect(string login, string password)
        {
            _cmd.CommandText = "function_check_if_credentials_correct";
            _cmd.CommandType = CommandType.StoredProcedure;
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

        public string GetRole(string login, string password)
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
    }
}
