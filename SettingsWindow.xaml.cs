using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using Text_reader.Database_operations;



//using Database;
//using Text_reader.Database;
//using System.Data.SQLite;
namespace Text_reader
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            ValuesTable voicesTable = new ValuesTable("voices");

           // List<string> voicesList = new List<string>();

            foreach(var i in voicesTable.GetData())
            {
                SetVoiceCB.Items.Add(i.ToString());
            }

            ValuesTable ratiosTable = new ValuesTable("ratios");

            foreach(var i in ratiosTable.GetData())
            {
                SetRatioCB.Items.Add(i.ToString());
            }

            ValuesTable volumesTable = new ValuesTable("volumes");

            foreach(var i in volumesTable.GetData())
            {
                SetVolumeCB.Items.Add(i.ToString());
            }

            SettingBaseTable baseTable = new SettingBaseTable();

            this.Closing += Time_Closing; ;
            
        }

        private void Time_Closing(object sender, System.ComponentModel.CancelEventArgs e)//В будущем удалить
        {
            //System.IO.File.Delete("Databases\\settings db\\settings.db");
        }

    }


}
