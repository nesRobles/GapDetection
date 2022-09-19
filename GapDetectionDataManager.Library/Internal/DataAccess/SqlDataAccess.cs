using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace GapDetectionDataManager.Library.Internal.DataAccess
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        
        private readonly IConfiguration _config;
        public static string GetConnectionString(string name)
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            string? connection =  ConfigurationManager.AppSettings.Get(name);

            return connection!;
        }

        public SqliteDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            //string connectionString = _config.GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SQLiteConnection(GetSQLConnectionString()))
            {
                List<T> models = connection.Query<T>(storedProcedure, parameters, commandType: commandType).ToList();

                return models;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            //string connectionString = _config.GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SQLiteConnection(GetSQLConnectionString()))
            {
                connection.Execute(storedProcedure, parameters, commandType: commandType);
            }
        }

        private static string GetSQLConnectionString()
        {
            SQLiteConnectionStringBuilder cs = new SQLiteConnectionStringBuilder();

            string sqlitePath = string.Empty;
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)!;

            cs.DataSource = Path.Combine(path, "GapDetectionDB.db");
            cs.Version = 3;
            sqlitePath = cs.ConnectionString;

            return sqlitePath;
        }
    }
}
