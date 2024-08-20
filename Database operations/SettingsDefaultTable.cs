using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_reader.Database_operations
{
    internal class SettingsDefaultTable : SettingsTable
    {
        FileInfo dbFile;
        public SettingsDefaultTable() : base()
        {

            TableName = "base";

           



            if (!exists)
            {
               

                

                MakeTableFull();
            }

        }
      
    }
}
