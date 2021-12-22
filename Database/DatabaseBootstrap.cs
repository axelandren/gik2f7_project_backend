using System;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ProjektWebApi.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private DatabaseConfig databaseConfig;
        public DatabaseBootstrap(DatabaseConfig dbConfig)
        {
            databaseConfig = dbConfig;
        }
        public void Setup()
        {
            // establish connection to database
            using(SqliteConnection connection = new SqliteConnection(databaseConfig.Name))
            {
                // check if table exists and get name
                var table = connection.Query<string>("SELECT Name FROM sqlite_master WHERE type='table' AND name='Games'");
                var tableName = table.FirstOrDefault();
                // if table name is not null and named "Games" then return
                if(!string.IsNullOrEmpty(tableName) && tableName == "Games")
                {
                    return;
                }
                // if null or wrong name, read database commands from file and execute on database
                using(var sr = new StreamReader(databaseConfig.StructureFile))
                {
                    var queries = sr.ReadToEnd();
                    connection.Execute(queries);
                }
            }
        }
    }
}
