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
               // connection.Close();


            }
        }
        public override List<object> GetData()
        {

            List<object> list = new List<object>();

            using (connection = new SQLiteConnection(dbSource + DbName))
            {
                connection.Open();

              

                using (SQLiteCommand selectCommand = new SQLiteCommand(connection))
                {
                    selectCommand.CommandText = "SELECT * FROM current";


                    using (SQLiteDataReader selectReader = selectCommand.ExecuteReader())
                    {
                        while (selectReader.Read())
                        {
                           list.Add( selectReader.GetValue(0));
                            list.Add(selectReader.GetValue(1));
                            list.Add(selectReader.GetValue(2));
                        }
                    }
                }
                connection.Close();
            }
            return list;

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
