using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
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


    }
}
