using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Text_reader.Database_operations
{
    internal class ValuesTable:DatabaseTable
    {
        public ValuesTable(string TableName)
        {
            this.TableName = TableName;

            DbName = @"Databases\values.sqlite3";

          

            connection = new System.Data.SQLite.SQLiteConnection("Data Source="+DbName);



        }


    }
}
