using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal abstract class MajorTable : MinorTable
    {

        protected abstract string SetTableName();


        public MajorTable()
        {
            TableName = SetTableName();



            connection.Open();

            query = "CREATE TABLE IF NOT EXISTS " + TableName + " (TEXT voice,INTEGER volume,REAL ratio)";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.ExecuteNonQuery();





            connection.Close();
        }

        public abstract void InsertDataToDotherTable();

    }
}
