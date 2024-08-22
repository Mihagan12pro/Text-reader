using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Text_reader.Database_operations
{
    internal class SettingsCurrentTable : AbstractSettingsTable
    {
        public readonly int Volume, Rate;
        public readonly string Voice;

       

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

                using (SQLiteConnection connection2 = new SQLiteConnection(dbSource + DbName))
                {
                    connection2.Open();
                    using (SQLiteCommand selectCommand = new SQLiteCommand(connection2))
                    {
                        selectCommand.CommandText = "SELECT * FROM current";
                        using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Voice = reader.GetString(0);
                                Volume =Convert.ToInt32( reader.GetValue(1));
                                Rate = Convert.ToInt32(reader.GetValue(2));
                            }
                        }
                    }
                    connection2.Close();
                }
               

            }
        }
       
        protected override void MakeTableFull()
        {
            using (connection = new SQLiteConnection(dbSource + DbName))
            {


                string voice="";
                int volume=0;
                double rate=0;

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
                        rate = Convert.ToInt32(columnList[2]);     

                        
                        
                       
                        
                      }

                     using (SQLiteCommand createCommand = new SQLiteCommand(connection))
                     {
                        createCommand.CommandText = $"CREATE TABLE {TableName} (voice TEXT, volume INTEGER,rate REAL)";
                        createCommand.ExecuteNonQuery();

                        using (SQLiteCommand insertCommand = new SQLiteCommand(connection))
                        {
                            insertCommand.CommandText = $"INSERT INTO {TableName} VALUES('{voice}',{volume},{rate})";
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
