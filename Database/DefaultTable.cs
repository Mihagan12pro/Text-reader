using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class DefaultTable:SettingsMajorTable
    {
        public override object GetData()
        {
            return null;
        }
        public DefaultTable() 
        {
            
           

            connection.Open();

            query = "CREATE TABLE IF NOT EXISTS " + TableName + " (TEXT voice,INTEGER volume,REAL ratio)";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.ExecuteNonQuery();



            query = "";

            connection.Close();

        }

        public override void InsertDataToDotherTable()
        {
            throw new NotImplementedException();
        }

        protected override string SetTableName()
        {
            return "default_settings";
        }
    }
}
