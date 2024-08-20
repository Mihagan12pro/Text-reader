using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;


namespace Text_reader.Database_operations
{
    internal class ValuesTable:AbstractDatabaseTable
    {
        public ValuesTable(string TableName)
        {
            this.TableName = TableName;

            DbName = @"Databases\values.sqlite3";

          

            connection = new System.Data.SQLite.SQLiteConnection("Data Source="+DbName);



        }

        public   object GetBaseData()
        {
            object data=null;

            connection.Open();

            using (SQLiteCommand command1 = new SQLiteCommand("SELECT * FROM " +TableName+ " LIMIT 1", connection))
            {
                using(SQLiteDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data = reader.GetValue(0);
                        return data;
                    }
                }
            }
            connection.Close();

          return data;
        }


    }
}
