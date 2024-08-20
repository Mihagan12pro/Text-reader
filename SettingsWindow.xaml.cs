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
    public partial class ConfigurationsWindow : Window
    {
        public ConfigurationsWindow()
        {
            InitializeComponent();

            ValuesTable voicesTable = new ValuesTable("voices");

           // List<string> voicesList = new List<string>();

            List<string> voices = new List<string>();
            List<int> volumes = new List<int>();
            List<double> ratios = new List<double>();

            foreach(var i in voicesTable.GetData())
            {
                //SetVoiceCB.Items.Add(i.ToString());


                voices.Add(i.ToString());
            }
            
            ValuesTable ratiosTable = new ValuesTable("ratios");

            foreach(var i in ratiosTable.GetData())
            {
               // SetRatioCB.Items.Add(i.ToString());

                ratios.Add(Convert.ToDouble(i.ToString()));
            }

            ValuesTable volumesTable = new ValuesTable("volumes");

            foreach(var i in volumesTable.GetData())
            {
                // SetVolumeCB.Items.Add(i.ToString());


                volumes.Add(Convert.ToInt32(i.ToString()));
            }


            voices.Sort();
            volumes.Sort();
            ratios.Sort();

            

            //SettingsDefaultTable baseTable = new SettingsDefaultTable();

            this.Closing += Time_Closing;

            SettingsCurrentTable currentTable = new SettingsCurrentTable();

            

            currentTable.AddToCombobox(voices.ConvertAll(item => (object)item), SetVoiceCB);
            currentTable.AddToCombobox(volumes.ConvertAll(item => (object)item), SetVolumeCB);
            currentTable.AddToCombobox(ratios.ConvertAll(item => (object)item), SetRatioCB);
            
        }

        private void Time_Closing(object sender, System.ComponentModel.CancelEventArgs e)//В будущем удалить
        {
            //System.IO.File.Delete("Databases\\settings db\\settings.db");
        }

    }


}
