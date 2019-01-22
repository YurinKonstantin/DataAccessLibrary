using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Common;


namespace DataAccessLibrary
{
    public static class DataAccess
    {
        public static void InitializeDatabase()
        {
            string databaseName = @"C:\\project\\sqliteSample.db";
         
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Text_Entry NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
                db.Close();
            }
        }

        public static void AddData(string inputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();
                for (int i = 0; i < 100; i++)
                {
                    SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                
                    // Use parameterized query to prevent SQL injection attacks
                    insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                

                    insertCommand.Parameters.AddWithValue("@Entry", inputText);
                    insertCommand.ExecuteReader();
                }
             //   insertCommand.Parameters.AddWithValue("@Entry", inputText);

              

                db.Close();
            }

        }
        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from MyTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }
    }
}
