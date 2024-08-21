using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Windows.Controls;
namespace Text_reader.Database_operations
{
    internal abstract class AbstractSettingsTable:AbstractDatabaseTable
    {
        protected bool exists = true;
        public AbstractSettingsTable()
        {
            FileInfo file = new FileInfo("..\\..\\Database operations\\settings.db");


            DbName = file.FullName;


            if (!System.IO.File.Exists(DbName))
            {
                exists = false;
               

             
            }
        }
        protected virtual void MakeTableFull()
        {



            SQLiteConnection conn = new SQLiteConnection("Data Source="+DbName);


                conn.Open();

            using (SQLiteCommand com = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS {TableName}(voice TEXT,volume INTEGER,rate INTEGER    )", conn))
            {
                com.ExecuteNonQuery();

                ValuesTable tableVoice = new ValuesTable("voices");
                ValuesTable tableRatio = new ValuesTable("rates");
                ValuesTable tableVolume = new ValuesTable("volumes");

                string baseVoice = Convert.ToString(tableVoice.GetBaseData());
                var baseRatio = Convert.ToDouble(tableRatio.GetBaseData());
                var baseVolume = Convert.ToInt32(tableVolume.GetBaseData());

     


                SQLiteCommand com2 = new SQLiteCommand("INSERT INTO " + TableName + $" (voice,volume,rate) Values('{baseVoice}',{Convert.ToInt32(baseVolume)},{Convert.ToInt32(baseRatio)})", conn);

                com2.ExecuteNonQuery();




                conn.Close();

            }


        }


       public void UpdateItself(string row,string data)
        {
            int i;
            
            switch (row)
            {
                case "voice":
                    i = 0;
                    break;

                case "volume":
                    i = 1;
                    break;

                case "rate":
                    i = 2;
                    break;
                   
                default:
                    return;
            }

            using (connection = new SQLiteConnection(dbSource+DbName))
            {
                connection.Open();

                using (SQLiteCommand selectCommand = new SQLiteCommand(connection))
                {
                    selectCommand.CommandText =$"SELECT * FROM {TableName}";

                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string value = reader.GetValue(i).ToString();

                            if (value != data)
                            {
                                using (SQLiteCommand updateCommand = new SQLiteCommand(connection))
                                {
                                    int iData;

                                    if (Int32.TryParse(data, out iData))
                                    {

                                        updateCommand.CommandText = $"UPDATE {TableName} SET {row}={Convert.ToInt32(data)}";
                                    }
                                    else
                                    {
                                       

                                        updateCommand.CommandText = $"UPDATE {TableName} SET {row}={data}";
                                    }


                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                        

                connection.Close();
            }


        }


    }
}
