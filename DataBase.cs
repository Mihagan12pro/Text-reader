using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Xml.Linq;
using System.IO;
namespace Text_reader
{
    //internal   class DataBase
    //{
    //   // SqliteConnection connection = new SqliteConnection("speaker_settings.db");


    //}

    class DBConstDefaultTable : DBTable
    {
        protected override string SetName()
        {
            return "const_default_settings";
        }

        public void AbortDefault()
        {
            SqliteCommand command = new SqliteCommand();

          
        }

    }

    abstract class DBTable
    {
        public SqliteConnection connection;
        protected SpeechSynthesizer synthesizer;
        protected DBTable table;

        protected readonly string tableName;

        protected string volume,speed,voice;


        protected abstract string SetName();
    
        public DBTable() 
        {
            tableName = SetName();


            


            if (File.Exists("./speaker_settings.sqlite3"))
            {
                File.Create("speaker_settings.sqlite3");
            }

            connection = new SqliteConnection("speaker_settings.sqlite3");

            //synthesizer = new SpeechSynthesizer();

            //table = this;




        }

        public string []GetVoiceSettings()
        {
            SqliteCommand command = new SqliteCommand();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
               volume =Convert.ToString( reader.GetInt32(0));
               speed = Convert.ToString( reader.GetDouble(1));
               voice = reader.GetString(2);
            }


            string[]arr = { volume, speed, voice };

            return arr;
        }

       

        public void Distructor()
        {
            connection.Close();
            table = null;


        }
    }

}
