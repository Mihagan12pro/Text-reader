using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal class SettingsCurrentTable : AbstractSettingsTable
    {
        public SettingsCurrentTable()
        {
            TableName = "current";


            if (!exists)
            {
                SettingsDefaultTable settingsDefault = new SettingsDefaultTable();
            }

            using (connection = new System.Data.SQLite.SQLiteConnection("Data Source="+DbName))
            {
                connection.Open();

                using (SQLiteCommand checkTable = new SQLiteCommand(connection))
                {
                    checkTable.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='current'";


                    var result = checkTable.ExecuteScalar();

                    if (result ==null)
                    {
                       

                        MakeTableFull();
                    }

                  
                }


            }
        }

        protected override void MakeTableFull()
        {
            using (connection = new SQLiteConnection(dbSource + DbName))
            {


                string voice="";
                int volume=0;
                double ratio=0;

                using (SQLiteCommand selectCommand = new SQLiteCommand(connection))
                {
                    selectCommand.CommandText = "SELECT * FROM base";

                    connection.Open();

                     using (SQLiteDataReader readSelected = selectCommand.ExecuteReader())
                      {
                        
                        List<object> columnList = new List<object>();

                        int row = 0;
                        while (readSelected.Read())
                        {
                            


                            columnList.Add(readSelected[0]);
                            columnList.Add(readSelected[1]);
                            columnList.Add(readSelected[2]);


                        }
                        voice = columnList[0].ToString();
                        volume = Convert.ToInt32(columnList[1]);
                        ratio = (double)(columnList[2]);     

                        
                        
                       
                        
                      }

                     using (SQLiteCommand createCommand = new SQLiteCommand(connection))
                     {
                        createCommand.CommandText = $"CREATE TABLE {TableName} (voice TEXT, volume INTEGER,ratio REAL)";
                        createCommand.ExecuteNonQuery();

                        using (SQLiteCommand insertCommand = new SQLiteCommand(connection))
                        {
                            insertCommand.CommandText = $"INSERT INTO {TableName} VALUES('{voice}',{volume},{ratio})";
                            insertCommand.ExecuteNonQuery();
                        }
                     }
                    connection.Close();
                }



             

                //connection.Close();
            }
        }
    }
}
