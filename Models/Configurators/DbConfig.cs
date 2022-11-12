using System.IO;

namespace ConsoleTesting.Models.Configurators
{
    public static class DbConfig
    {
        public static string GetConnectionString(string path)
        {
            var connectionString = File.ReadAllText(path);
            return connectionString;
        }
    }
}
