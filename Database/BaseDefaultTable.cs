using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class BaseDefaultTable : SettingsMajorTable
    {
        protected override string SetTableName()
        {
            return "base_settings";
        }
        public override void InsertDataToDotherTable()
        {

        }
        public override object GetData()

        {
            connection.Open();

            query = "SELECT * FROM " + TableName;


            SQLiteCommand command = new SQLiteCommand(query, connection);
            var a = command.ExecuteNonQuery();
            return a;
        }
        public BaseDefaultTable()
        {
            connection.Open();


            query = "CREATE TABLE IF NOT EXISTS "+TableName+"(voice TEXT, ratio REAL, volume INTEGER)";

            SQLiteCommand command1 = new SQLiteCommand(query,connection);

            command1.ExecuteNonQuery();

            query = "SELECT COUNT (*) FROM " + TableName;
           
           using (SQLiteCommand command2 = new SQLiteCommand(query,connection))
            {
               int count  =   command2.ExecuteNonQuery();

                if (count<1)
                {
                    query = "INSERT INTO "+TableName+"VALUES ( 'Microsoft Irina Desktop',1.0,100 )";

                    SQLiteCommand command3 = new SQLiteCommand(query,connection);

                    command3.ExecuteNonQuery();
                }
            }


            //query = "SELECT COUNT (*) FROM " + TableName;


            //SQLiteCommand command = new SQLiteCommand(query,connection);

            //int count = Convert.ToInt32(command.ExecuteScalar());

            //if(count > 1)
            //{
            //    query = "DELETE FROM "+TableName;

            //    SQLiteCommand command2 = new SQLiteCommand(query,connection);

            //    command2.ExecuteNonQuery();

            //}

            //if (count != 1)
            //{
            //    query = "INSERT INTO "+TableName+ " VALUES ( 'Microsoft Irina Desktop',100,1 )";


            //    SQLiteCommand command3 = new SQLiteCommand(query,connection);

            //    command3.ExecuteNonQuery();
            //}

            connection.Close();
        }

        





    }
}
