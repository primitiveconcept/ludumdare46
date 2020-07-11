namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Data.Sqlite;


    public static class GameDatabase
    {
        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(
                new SqliteConnectionStringBuilder
                    {
                        DataSource = "game.db"
                    }.ToString());
        }


        public static void CreateNewDatabase()
        {
            var connection = GetConnection();
            IEnumerable<Type> databaseTableTypes =
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                    .Where(
                        x => typeof(IDatabaseTable).IsAssignableFrom(x)
                             && !x.IsInterface
                             && !x.IsAbstract);

            foreach (var databaseTableType in databaseTableTypes)
            {
                StringBuilder sql = new StringBuilder($"CREATE TABLE {databaseTableType.Name} (");
                
                foreach (var property in databaseTableType.GetProperties())
                {
                    if (property.Name.ToLower() == "id")
                    {
                        sql.AppendLine("Id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY");
                    }
                    
                    if (property.GetSetMethod() == null)
                        continue;
                    
                    // TODO
                }
            }
        }
    }
}