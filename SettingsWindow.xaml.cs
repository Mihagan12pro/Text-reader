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
            


            

           // ValueTable ratioTable = new ValueTable("ratio_settings","ratio");
            //ValueTable volumeTable = new ValueTable("volume_settings");
            //ValueTable voiceTable = new ValueTable("voice_settings");

            //object[]  ratios = (object[]) ratioTable.GetData();
            //var volumes = (object[])volumeTable.GetData();
            //var voices = (object[])voiceTable.GetData();

            //var a = ratioTable.GetData();
            //var b = a;

            //foreach (var voice in voices)
            //{
            //    SetVoiceCB.Items.Add(voice);
            //}
            //foreach(var  volume in volumes)
            //{
            //    SetVolumeCB.Items.Add(volume);
            //}
            //foreach(var ratio in  ratios)
            //{
            //    SetRatioCB.Items.Add(ratio);
            //}

            //foreach(var vol in (int[])volumeTable.GetData())
            //{
            //    SetVolumeCB.Items.Add(Convert.ToString(vol));
            //}

            //for(double i=0.5;i<2.5;i+=0.5)
            //{
            //    SetSpeedCB.Items.Add(Convert.ToString(i));
            //}

            //foreach(var rat in (double[])ratioTable.GetData())
            //{
            //    SetRatioCB.Items.Add(Convert.ToString(rat));
            //}




            //SpeechSynthesizer speech = new SpeechSynthesizer();
            //foreach(var voice in speech.GetInstalledVoices())
            //{
            //    SetVoiceCB.Items.Add(voice.VoiceInfo.Name); 
            //}


           // BaseDefaultTable baseDefaultTable = new BaseDefaultTable();

            //var c = baseDefaultTable.GetData();
            //var c2 = c;
            //var a = defaultTable.GetVoiceSettings();
            //var b = a;
        }
    }
}
