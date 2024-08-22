using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal abstract class AbstractDatabaseTable
    {
        protected  int columnsCount;
        protected const string dbSource = "Data Source=";

        protected string getDataQuery = "";

        public string TableName { get; protected set; }
        public string DbName { get;protected set; }

       protected SQLiteConnection connection;

        public AbstractDatabaseTable()
        {
            columnsCount = 3;
        }


        public List<object> GetData()
        {
            List<object>data=new List<object>();

         using   (connection = new SQLiteConnection(dbSource+DbName))
            {

            connection.Open();



            using(SQLiteCommand command = new SQLiteCommand("SELECT * FROM "+TableName,connection))
            {

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for(int i=0;i< columnsCount;i++)
                        data.Add(reader.GetValue(i));
                    }



                }

            }
            connection.Close();
                }

            return data;
        }


    }
}
