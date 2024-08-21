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

            List<string> voices = new List<string>();
            List<int> volumes = new List<int>();
            List<double> ratios = new List<double>();

            foreach(var i in voicesTable.GetData())
            {
              //  SetVoiceCB.Items.Add(i.ToString());


                voices.Add(i.ToString());
            }
            
            ValuesTable ratiosTable = new ValuesTable("rates");

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
            foreach (var i in voices)
            {
                SetVoiceCB.Items.Add(i.ToString());


               // voices.Add(i.ToString());
            }

            

            foreach (var i in ratios)
            {
                SetRatioCB.Items.Add(i.ToString());

                //ratios.Add(Convert.ToDouble(i.ToString()));
            }


            foreach (var i in volumes)
            {
                SetVolumeCB.Items.Add(i.ToString());


               // volumes.Add(Convert.ToInt32(i.ToString()));
            }


            //SettingsDefaultTable baseTable = new SettingsDefaultTable();

            this.Closing += Time_Closing;

            SettingsCurrentTable currentTable = new SettingsCurrentTable();

            List<object> selectedDataList = currentTable.GetData();

            SetVoiceCB.SelectedItem = selectedDataList[0];

            SetVolumeCB.SelectedItem =Convert.ToString( selectedDataList[1]);

            SetRatioCB.SelectedItem =Convert.ToString( selectedDataList[2]);

//        SetVoiceCB.SelectedItem =    currentTable.GetCurrentData(volumes.ConvertAll(item => (object)item), SetVoiceCB);

////SetVoiceCB.SelectedItem=                currentTable.GetCurrentData(voices.ConvertAll(item => (object)item), SetVoiceCB);
 
//            var a = currentTable.GetCurrentData(ratios.ConvertAll(item => (object)item), SetRatioCB);
//            SetRatioCB.SelectedItem = a;

//            var b = a;
        }

        private void Time_Closing(object sender, System.ComponentModel.CancelEventArgs e)//В будущем удалить
        {
            //System.IO.File.Delete("Databases\\settings db\\settings.db");
        }

    }


}
