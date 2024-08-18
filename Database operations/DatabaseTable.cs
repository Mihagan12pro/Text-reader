using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal abstract class DatabaseTable
    {
        public string TableName { get; protected set; }
        public string DbName { get;protected set; }

       protected SQLiteConnection connection;

        public DatabaseTable()
        {
            
        }


        public virtual List<object> GetData()
        {
            List<object>data=new List<object>();
            connection.Open();
            using(SQLiteCommand command = new SQLiteCommand("SELECT * FROM "+TableName,connection))
            {

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetValue(0));
                    }



                }

            }
            connection.Close();

            return data;
        }


    }
}
