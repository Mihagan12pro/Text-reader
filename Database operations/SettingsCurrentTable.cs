﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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


        public void AddToCombobox(List<object>list,ComboBox combo)
        {
            int i;

            switch(combo.Name)
            {
                case "SetVoiceCB":
                    i = 0;
                    break;
                case "SetRatioCB":
                    i = 1;
                    break;
                case "SetVolumeCB":
                    i = 2;
                    break;
                default:
                    i = -1;
                    break;
            }
            if (i==-1)
            {
                MessageBox.Show("Database error!");
                return;
            }

            var datas = GetData()[i];

          
            int plus = 1;
            foreach (var item in list)
            {

                combo.Items.Add(item);

            }
            combo.SelectedItem = datas;
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
