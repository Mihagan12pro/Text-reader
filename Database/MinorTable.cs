using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal abstract class MinorTable
    {
        protected string query;

        
        protected SQLiteConnection connection;

        protected readonly string databaseFields;
        protected readonly string DatabaseTittle;


        protected string voice;
        protected int volume;
        protected double ratio;


        private MinorTable table;

        public string TableName { get; protected set; }



        public MinorTable()
        {

            databaseFields = "(voice TEXT, volume INTENGER, ratio REAL)";

            table = this;
            DatabaseTittle = "settings.sqlite3";

            connection = new SQLiteConnection("Data Source=" + DatabaseTittle);

           
        }
        public void Destructor()
        {
            table = null;
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
