using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Text_reader.Database;

namespace Database
{
    internal abstract class SettingsMinorTable: AbstractTable
    {
      

        protected readonly string databaseFields;
       


        protected string voice;
        protected int volume;
        protected double ratio;


        private SettingsMinorTable table;

      



        public SettingsMinorTable()
        {

            databaseFields = "(voice TEXT, volume INTENGER, ratio REAL)";

            table = this;
            DatabaseTittle = "settings.sqlite3";

            connection = new SQLiteConnection("Data Source=" + DatabaseTittle);

           
        }

       // public abstract void CreateTable();

        public void GetTableItems()
        {
            connection.Open();

            query = "SELECT * FROM "+TableName;

            SQLiteCommand  command = new SQLiteCommand(query,connection);

            var result =  command.ExecuteNonQuery();

            connection.Close();

        }

       
    }
}
