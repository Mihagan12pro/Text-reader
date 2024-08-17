using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SQLiteConnection connection = new SQLiteConnection("Data Source=settings.sqlite3");


            //SQLiteCommand command = new SQLiteCommand("CREATE TABLE base_settings (voice TEXT, volume INTENGER, ratio REAL)",connection);

            //connection.Open();

            //command.ExecuteNonQuery();

            //connection.Close();


            //CurrentTable currentTable= new CurrentTable();

            //currentTable.GetTableItems();

             BaseDefaultTable baseTable=new BaseDefaultTable();

            DefaultTable defaultTable = new DefaultTable();
           

             Console.ReadKey();
        }
    }
}
