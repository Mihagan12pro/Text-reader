using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal class SettingBaseTable : SettingsTable
    {
        FileInfo dbFile;
        public SettingBaseTable() : base()
        {

            TableName = "base";

           



            if (!exists)
            {
               

                

                MakeTableFull();
            }

        }
        //    protected override void MakeTableFull()
        //    {


        //        connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + dbFile.FullName);
        //        connection.Open();
        //        using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS base (voice TEXT,volume INTEGER,ratio REAL)",connection))
        //        {




        //            command.ExecuteNonQuery();

        //            SQLiteConnection connection2 = new SQLiteConnection("Data Source="+ @"..\..\Database operations\Databases\values.sqlite3");

        //            connection2.Open();

        //            SQLiteCommand command2 = new SQLiteCommand("SELECT * FROM ratio VALUES WHERE ratio = 1.0",connection2);

        //            int ratio = command2.ExecuteNonQuery();

        //            connection2.Close();
        //        }



        //        connection.Close();
        //    }

        //}
    }
}
