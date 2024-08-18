using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class CurrentTable :SettingsMinorTable
    {

       public override object GetData()
        {
            return null;
        }
        //public override void CreateTable()
        //{
        //    connection.Open();

        //    quary = "CREATE TABLE IF NOT EXIST " + TableName;

        //    SQLiteCommand  command = new System.Data.SQLite.SQLiteCommand(quary, connection);

        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

    }
}
