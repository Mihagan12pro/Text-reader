using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class BaseDefaultTable : MajorTable
    {
        protected override string SetTableName()
        {
            return "base_settings";
        }
        public override void InsertDataToDotherTable()
        {

        }

        public BaseDefaultTable()
        {
            connection.Open();

            query = "SELECT COUNT (*) FROM " + TableName;


            SQLiteCommand command = new SQLiteCommand(query,connection);

            int count = Convert.ToInt32(command.ExecuteScalar());

            if(count > 1)
            {
                query = "DELETE FROM "+TableName;

                SQLiteCommand command2 = new SQLiteCommand(query,connection);

                command2.ExecuteNonQuery();

            }
            
            if (count != 1)
            {
                query = "INSERT INTO "+TableName+ " VALUES ( 'Microsoft Irina Desktop',100,1 )";


                SQLiteCommand command3 = new SQLiteCommand(query,connection);

                command3.ExecuteNonQuery();
            }

            connection.Close();
        }

        





    }
}
