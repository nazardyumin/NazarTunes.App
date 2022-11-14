using Dapper;
using MySql.Data.MySqlClient;
using NazarTunes.Models.Configurators;

namespace NazarTunes.Models.MySQLConnections
{
    public abstract class AbsctractDb
    {
        protected readonly MySqlConnection _db;
        protected readonly MySqlCommand _cmd;

        protected AbsctractDb() 
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _db = new MySqlConnection(DbConfig.GetConnectionString("db.txt"));
            _cmd = new MySqlCommand { Connection = _db };
        }
    }
}
