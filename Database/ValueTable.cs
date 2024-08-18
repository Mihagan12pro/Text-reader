using System.Data.SQLite;
using System.Windows.Documents;
using System;
namespace Text_reader.Database
{
    internal class ValueTable : AbstractTable
    {


        private string columnName;
        public ValueTable(string TableName,string columnName) 
        {
            this.TableName = TableName;
            this.columnName = columnName;
            DatabaseTittle = @"values.db";

            connection = new SQLiteConnection("Data Source="+DatabaseTittle);


        }



        public override object GetData()
        {
            query = $"SELECT " + columnName + " FROM " + TableName;


            connection.Open();

            SQLiteCommand command =new SQLiteCommand(query,connection);
           

            var result = (command.ExecuteNonQuery());

            

           

            connection.Close();

            return result ;
        }
    }
}
