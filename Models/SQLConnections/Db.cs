using Dapper;
using MySql.Data.MySqlClient;
using NazarTunes.Models.Configurators;
using NazarTunes.Models.DataTemplates;
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
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("new_login", MySqlDbType.VarChar);
            _cmd.Parameters["new_login"].Value = login;
            var returnParameter = _cmd.Parameters.Add("@ReturnVal", MySqlDbType.Int32);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            _db.Open();
            _cmd.ExecuteNonQuery();
            _db.Close();
            return (int)returnParameter.Value == 1;
        }

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
            return new Admin {Id=(int)getId.Value, FirstName=(string)getFirstName.Value, LastName = (string)getLastName.Value, Role=role };
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
    }
}
