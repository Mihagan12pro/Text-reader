using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
namespace Text_reader.Database_operations
{
    internal abstract class SettingsTable:DatabaseTable
    {
        protected bool exists = true;
        public SettingsTable()
        {
            DbName = "Databases\\settings db\\settings.db";


            if (!System.IO.File.Exists(DbName))
            {
                exists = false;
               

             
            }
        }
        protected virtual void MakeTableFull()
        {

            SQLiteConnection conn = new SQLiteConnection("Data Source="+DbName);


                conn.Open();

                using (SQLiteCommand com = new SQLiteCommand("CREATE TABLE IF NOT EXISTS base(voice TEXT,volume INTEGER,ratio REAL)",conn))
                {
                    com.ExecuteNonQuery();

                    ValuesTable tableVoice = new ValuesTable("voices");
                    ValuesTable tableRatio = new ValuesTable("ratios");
                    ValuesTable tableVolume = new ValuesTable("volumes");

                    string baseVoice = tableVoice.GetBaseData();
                    double baseRatio =Convert.ToDouble( tableRatio.GetBaseData());
                    int baseVolume = Convert.ToInt32(tableVolume.GetBaseData());
                    
                    using (SQLiteConnection conn2 = new SQLiteConnection("Data Source="+tableVolume.DbName))
                    {
                         conn2.Open();


                        SQLiteCommand com2 = new SQLiteCommand("INSERT INTO "+TableName+ $"Values('{baseVoice}',{baseVolume},{baseRatio})",conn);

                        com2.ExecuteNonQuery();

                        conn2.Close();
                    }
                    
                }

                //double ratio;
                //int volume;
                //string voice;

              

                conn.Close();
            



        }


    }
}
