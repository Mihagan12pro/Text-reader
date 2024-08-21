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

         

            List<string> voices = new List<string>();
            List<int> volumes = new List<int>();
            List<double> ratios = new List<double>();

            foreach(var i in voicesTable.GetData())
            {
              


                voices.Add(i.ToString());
            }
            
            ValuesTable ratiosTable = new ValuesTable("rates");

            foreach(var i in ratiosTable.GetData())
            {
             

                ratios.Add(Convert.ToDouble(i.ToString()));
            }

            ValuesTable volumesTable = new ValuesTable("volumes");

            foreach(var i in volumesTable.GetData())
            {
                


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
                SetRateCB.Items.Add(i.ToString());

                //ratios.Add(Convert.ToDouble(i.ToString()));
            }


            foreach (var i in volumes)
            {
                SetVolumeCB.Items.Add(i.ToString());


               // volumes.Add(Convert.ToInt32(i.ToString()));
            }


            //SettingsDefaultTable baseTable = new SettingsDefaultTable();

            this.Closing += Closing_message;

            SettingsCurrentTable currentTable = new SettingsCurrentTable();

            List<object> selectedDataList = currentTable.GetData();


            SetVoiceCB.SelectedItem = selectedDataList[0];
            SetVolumeCB.SelectedItem =Convert.ToString( selectedDataList[1]);
            SetRateCB.SelectedItem =Convert.ToString( selectedDataList[2]);


        }

        private void Closing_message(object sender, System.ComponentModel.CancelEventArgs e)//В будущем удалить
        {
            SettingsCurrentTable currentTable = new SettingsCurrentTable();

            string dbVoice =   Convert.ToString( currentTable.GetData()[0]);
            string dbVolume = Convert.ToString(Convert.ToInt32(currentTable.GetData()[1]));
            string dbRate = Convert.ToString(Convert.ToInt32(currentTable.GetData()[2]));


            //var a = dbVoice;
            //var b1 = SetVoiceCB.SelectedValue;
            
            //var b2 = b1;


            //bool c1 = a.Equals(b1);
            //bool c2 = c1;

            if (!dbRate.Equals( SetRateCB.SelectedValue) || !dbVoice.Equals( SetVoiceCB.SelectedValue) || !dbVolume.Equals(SetVolumeCB.SelectedValue))
            {
                var answer = MessageBox.Show("Are you sure that you don't want to save new settings?", "", MessageBoxButton.YesNo);

                if (answer == MessageBoxResult.No)
                    e.Cancel = true;



                return;



            }

        }









        private void AcceptSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsCurrentTable currentTable = new SettingsCurrentTable();

            currentTable.UpdateItself("voice",SetVoiceCB.SelectedItem.ToString());
            currentTable.UpdateItself("volume", SetVolumeCB.SelectedItem.ToString());
            currentTable.UpdateItself("rate", SetRateCB.SelectedItem.ToString());
        }
    }


}
