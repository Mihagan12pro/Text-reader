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
    internal class ValuesTable:DatabaseTable
    {
        public ValuesTable(string TableName)
        {
            this.TableName = TableName;

            DbName = @"Databases\values.sqlite3";

          

            connection = new System.Data.SQLite.SQLiteConnection("Data Source="+DbName);



        }

        public  string GetBaseData()
        {
            string data;

            connection.Open();

            using (SQLiteCommand command1 = new SQLiteCommand("SELECT * FROM " +TableName+ " WHERE rowid=0",connection))
            {
                using(SQLiteDataReader reader = command1.ExecuteReader())
                {
                  
                        data = reader.ToString();
                    
                 
                }
            }
            connection.Close();

            return data;
        }


    }
}
