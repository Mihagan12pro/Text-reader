using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal class SettingsDefaultTable : AbstractSettingsTable
    {
        
        public SettingsDefaultTable() : base()
        {

            TableName = "base";

           



            if (!exists)
            {

               // File.Create();
                

                MakeTableFull();
            }

        }

        public object[] AbortCurrentUpdate()
        {
            object[] oldData = new object[3];



            using(SQLiteConnection conn = new SQLiteConnection(dbSource+DbName))
            {
                conn.Open();


                using (SQLiteCommand updateCommand = new SQLiteCommand(conn))
                {
                    string voice =Convert.ToString( GetData()[0]);
                    int volume = Convert.ToInt32(GetData()[1]);
                    int rate = Convert.ToInt32(GetData()[2]);

                    updateCommand.CommandText = $"UPDATE current SET voice='{voice}',volume={volume},rate={rate} WHERE rowid={1} ";

                    oldData[0] = voice;
                    oldData[1] = volume;
                    oldData[2] = rate;

                    updateCommand.ExecuteNonQuery();
                }

                    conn.Close();
            }
            return oldData;
        }
    }
}
