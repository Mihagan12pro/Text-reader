using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database
{
    internal abstract class AbstractTable
    {
        protected string query;

        protected  string DatabaseTittle;

        protected SQLiteConnection connection;



        public string TableName { get; protected set; }


        public abstract object GetData();

    }
}
