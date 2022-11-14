using MySql.Data.MySqlClient;
using System.Data;

namespace NazarTunes.Models.MySQLConnections
{
    public class AuthorizationSectionDb : AbsctractDb
    {
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
    }
}
